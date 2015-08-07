using UnityEngine;
using System.Collections;
using TowerScripts;
using Google2u;

public class MF_FireWeapon : MonoBehaviour {
	
	[Header("Shot settings:")]
	public ParticleSystem shot;
	public AudioSource shotSound;
	[Tooltip("(deg radius)\nAdds random inaccuracy to shots.\n0 = perfect accuracy.")]
	public float inaccuracy; // in degrees
	[Header("Set 2 of 3, and the 3rd will be computed")]
	[Tooltip("(meters/sec)")]
	public float shotSpeed;
	[Tooltip("(meters)")]
	public float maxRange;
	[Tooltip("(seconds)")]
	public float shotDuration;
	[Header("Fire rate settings:")]
	[Tooltip("(seconds)\nThe minimum time between shots.\n0 = every frame.")]
	public float cycleTime; // min time between shots
	[Tooltip("Number of shots before a Burst Reset Time is triggered. (Seperate from Reload Time.)\n0 = Burst Reset Time is ignored.")]
	public int burstAmount; // 0 = unlimited burst
	[Tooltip("(seconds)\nThis should be more than Cycle Time, and less than Reload Time. " +
	         "Shots cannot fire quicker than Cycle Time, and Reload Time takes precedence over Burst Reset Time if they occur on the same shot.")]
	public float burstResetTime; // should be <= reloadTime
	[Tooltip("Multiple shots per fire command. (Like a shotgun blast.)")]
	public int shotsPerRound = 1;
	[Header("Ammo settings")]
	[Tooltip("Triggered when Max Ammo Count is 0.")]
	public float reloadTime;
	public bool dontReload;
	[Header("Other Settings:")]
	[Tooltip("(deg radius)\nIncrease to apparent target size. Used to cause a weapon to begin firing before completely aimed at target.")]
	public float aimTolerance; 
	[Header("Exit point(s) of shots")]
	[Tooltip("If multiple exits, they will be used sequentially. (Usefull for a missile rack, or multi-barrel weapons)")]
	public GunExit[] exits;
	
	
	public NozzleRecoilBase recoiler;
	[HideInInspector] public Vector3 platformVelocity;
	[HideInInspector] public int curAmmoCount;
	[HideInInspector] public int curBurstCount;
	[HideInInspector] public int curExit;
	[HideInInspector] public float curInaccuracy;
	
	float delay;
	float lastLosCheck;
	bool losClear;
	bool error;
	
	[System.Serializable]
	public class GunExit {
		public Transform transform;
		[HideInInspector] public GameObject flare;
		[HideInInspector] public ParticleSystem particleComponent;
	}
	
	void OnValidate() {
		curInaccuracy = inaccuracy;
	}
	
	public void Start () {
		if (CheckErrors() == true) { return; }
		
		
		// find muzzle flash particle systems
		for (int f=0; f < exits.Length; f++) {
			if ( exits[f].transform.childCount == 1 ) { // check for child
				exits[f].flare = exits[f].transform.GetChild(0).gameObject;
				exits[f].particleComponent = exits[f].flare.GetComponent<ParticleSystem>();
				exits[f].particleComponent.Stop(true);
			}
		}
		
		curInaccuracy = inaccuracy; // initialize
		
		// compute missing value: shotSpeed, maxRange, shotDuration
		if (shotSpeed <= 0) {
			shotSpeed = maxRange / shotDuration;
		} else if (maxRange <= 0) {
			maxRange = shotSpeed * shotDuration;
		} else { // compute shotDuration even if all 3 are set to keep math consistant
			shotDuration = maxRange / shotSpeed;
		}
	}
	
	// use this to fire weapon
	public void Shoot () {
		if ( ReadyCheck() == true ) {
			DoFire();
		}
	}
	
	// use this only if already checked if weapon is ready to fire
	public virtual void DoFire () {
		if (error == true) { return; }
		shot.enableEmission = true;
		shot.emissionRate = 100;
	}
	public void DontShoot() {
		shot.enableEmission = false;
		shot.emissionRate = 0;
	}

	public void initWeapon(TowerListRow aData) {
		ParticleCollision p = shot.GetComponent<ParticleCollision>();
		p.damageToInflict = aData._BulletDamage;
		p.infantryDamageMultiplier = aData._BulletInfantryModifier;
		p.tankInfantryMultiplier = aData._BulletArmorModifier;

	}
	// use to determine if weapon is ready to fire. (not realoding, waiting for cycleTime, etc.) Seperate function to allow other scripts to check ready status.
	public virtual bool ReadyCheck () {
		if ( curAmmoCount <= 0 && dontReload == true) {
			// out of ammo
		} else {
			if ( Time.time >= delay ) {
				return true;
			}
		}
		return false;
	}
	
	// check if the weapons is aimed at the target location - does not account for shot intercept point. Use the AimCheck in the SmoothTurret script for intercept. This is here to use the weapon script without a turret.
	public virtual bool AimCheck ( Transform target, float targetSize ) {
		bool _ready = false;
		if (target) {
			float _targetRange = Vector3.Distance(exits[curExit].transform.position, target.position);
			float _targetFovRadius = Mathf.Clamp(   (Mathf.Atan( (targetSize / 2) / _targetRange ) * Mathf.Rad2Deg) + aimTolerance,    0, 180 );
			if ( Vector3.Angle(exits[curExit].transform.forward, target.position - exits[curExit].transform.position) <= _targetFovRadius ) {
				_ready = true;
			}
		}
		return _ready;
	}
	
	public virtual bool CheckErrors () {
		error = false;
		string _object = gameObject.name;
		if (shotsPerRound <= 0) { Debug.Log(_object+": Weapon shotsPerRound must be > 0."); error = true; }
		if (exits.Length <= 0) { Debug.Log(_object+": Weapon must have at least 1 exit defined."); error = true; }
		for (int e=0; e < exits.Length; e++) {
			if (exits[e].transform == false) { Debug.Log(_object+": Weapon exits index "+e+" hasn't been defined."); error = true; }
		}
		int _e1 = 0;
		if (shotSpeed <= 0) { _e1++; }
		if (maxRange <= 0) { _e1++; }
		if (shotDuration <= 0) { _e1++; }
		if (_e1 > 1) {
			maxRange = 1f; // prevent div 0 error if another script accesses maxRange
			Debug.Log(_object+": 2 of 3 need to be > 0: shotSpeed, maxRange, shotDuration");
			error = true;
		}
		for (int f=0; f < exits.Length; f++) {
			if ( exits[f].transform ) {
				if ( exits[f].transform.childCount > 1 ) { Debug.Log(_object+": Weapon exits index "+f+" must not have more than 1 child."); error = true; }
				if ( exits[f].transform.childCount > 0 ) {
					exits[f].flare = exits[f].transform.GetChild(0).gameObject;
					if ( !exits[f].flare.GetComponent<ParticleSystem>() ) { Debug.Log(_object+": Weapon exits index "+f+" child has no ParticleSystem."); error = true; }
				}
			}
		}
		
		return error;
	}
}

using UnityEngine;
using System.Collections;
using TowerScripts;
using DigitalRuby.ThunderAndLightning;

public class MF_ElectroWeapon : MonoBehaviour {

	[Header("Shot settings:")]
	public LightningBoltPrefabScript shot;
	public string shotName;
	public AudioSource shotSound;
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
	[Header("Exit point(s) of shots")]
	[Tooltip("If multiple exits, they will be used sequentially. (Usefull for a missile rack, or multi-barrel weapons)")]
	public GunExit[] exits;
	

	public float maxRange = 100f;
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
		
	}
	
	// use this to fire weapon
	public void Shoot (GameObject aTarget) {
		if ( ReadyCheck() == true ) {
			DoFire(aTarget);
		}
	}
	
	// use this only if already checked if weapon is ready to fire
	public virtual void DoFire (GameObject aTarget) {
		if (error == true) { return; }
		// reset burst and ammo, delay has already happened
		if (curAmmoCount <= 0) {
			curBurstCount = burstAmount;
		//	curExit = 0;
		}
		if (curBurstCount <= 0) { curBurstCount = burstAmount; }
		
		// fire weapon
		// create shot
		for (int spr=0; spr < shotsPerRound; spr++) {
			GameObject myShot = null;
			if(recoiler!=null)
				recoiler.recoil();
			shot.Destination = aTarget;
			shot.Source = exits[curExit].transform.gameObject;
			shot.CallLightning();
			/*if(myShot==null) {
				myShot = (GameObject) Instantiate(shot.Template, exits[curExit].transform.position, exits[curExit].transform.rotation);
			}*/
			
		} 
		// flare
		if (exits[curExit].flare) {
			exits[curExit].particleComponent.Play();
		}
		// audio
		if (shotSound) {
			shotSound.Play();
		}
		 
		curBurstCount--; 
		curExit = MFmath.Mod(curExit+1, exits.Length); // next exit
		
		// find net delay
		delay = Time.time + cycleTime;
		if (curBurstCount <= 0 && burstAmount != 0) { // 0 = unlimited burst
			delay = Time.time + Mathf.Max(cycleTime, burstResetTime); // burstResetTime cannot be < cycleTime
		}
		if (curAmmoCount <= 0 ) { 
			delay = Time.time + reloadTime;
		}
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
		return true;
	}
	
	public virtual bool CheckErrors () {
		error = false;
		string _object = gameObject.name;
		if (shot == false) { Debug.Log(_object+": Weapon shot object hasn't been defined."); error = true; }
		if (shotsPerRound <= 0) { Debug.Log(_object+": Weapon shotsPerRound must be > 0."); error = true; }
		if (exits.Length <= 0) { Debug.Log(_object+": Weapon must have at least 1 exit defined."); error = true; }
		for (int e=0; e < exits.Length; e++) {
			if (exits[e].transform == false) { Debug.Log(_object+": Weapon exits index "+e+" hasn't been defined."); error = true; }
		}
		int _e1 = 0;
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

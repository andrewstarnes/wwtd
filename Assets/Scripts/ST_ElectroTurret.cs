using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MF_BasicScanner))]
[RequireComponent (typeof (MF_TargetList))]
[RequireComponent (typeof (ST_ElectroTurretControl))]
[RequireComponent (typeof (MF_BasicTargeting))] 
public class ST_ElectroTurret : MF_AbstractPlatform {
	[Tooltip("Turret will aim ahead to hit a moving target.")]
	public bool useIntercept = true;

	[Header("Aiming error:")]
	[Tooltip("(deg radius)\nWill cause the turret to point in a random offset from target. Recomend value less than 1.")]
	public float aimError; // 0 = no error, will skip aimError routine if turningAimInaccuracy is also 0
	[Tooltip("How often to generate a new error offset.\n0 = every frame, but only if Aim Error or Turning Aim Inaccuracy are greater than 0.")]
	public float aimErrorInterval = 0f;
	[Header("Turret aim inaccuracy due to turn/elevate: ")]
	
	[Header("Object holding turret:")]
	[Tooltip("Will impart any velocity to fired shots. Rigidbody is not necessary. If turret platform/vehicle/parent is stationary, may leave empty:")]
	public GameObject platform;

	[HideInInspector] public float targetRange;
	[HideInInspector] public Vector3 platformVelocity;
	[HideInInspector] public Vector3 systAimError;
	[HideInInspector] public float averageRotRateEst;
	[HideInInspector] public float averageEleRateEst;
	[HideInInspector] public float totalTurnWeapInaccuracy;
	[HideInInspector] public float totalTurnAimInaccuracy;
	
	MF_BasicScanner scannerScript;
	Vector3 lastTargetPosition;
	Vector3 lastPlatformPosition;
	float lastAimError;
	bool error;
	
	void Start () {
		if (CheckErrors() == true) { return; }
	}
	
	void Update () {
		if (error == true) { return; }
		
		// find rotation/elevation rates
		float _useTime;
		if (Time.time < 1) { // to avoid smoothDeltaTime returning NaN on the first few frames
			_useTime = Time.deltaTime;
		} else {
			_useTime = Time.smoothDeltaTime;
		}
		// average rotation, then move to lastRotation
		
		// move aim error location
		if ( aimError + totalTurnAimInaccuracy > 0 ) {
			if ( Time.time >= lastAimError + aimErrorInterval ) {
				systAimError = Random.insideUnitSphere;
				lastAimError = Time.time;
			}
		}
		
		// intercept
		if (target) {
			if ( useIntercept == true && shotSpeed > 0 ) { // point at shot and target intercept location

				platformVelocity = Vector3.zero;
				

				Vector3 _targetVelocity;
				if (target.GetComponent<Rigidbody>()) { // if target has a rigidbody, use velocity
					_targetVelocity = target.GetComponent<Rigidbody>().velocity;
				} else { // otherwise compute velocity from change in position
					_targetVelocity = (target.transform.position - lastTargetPosition) / Time.deltaTime;
					lastTargetPosition = target.transform.position;
				}
				// point at linear intercept position
				targetLocation = MFcompute.Intercept(weaponMount.transform.position, platformVelocity, shotSpeed, target.transform.position, _targetVelocity);
				
			} else { // point at target position
				targetLocation = target.transform.position;
			}
		}
		
		// find aim locations
		Vector3 _localTarget;
		float _xzDist;
		if (target) {
			targetRange = Vector3.Distance( weaponMount.transform.position, target.transform.position );
		} else { // no target
			// set rotation and elevation goals to the rest position
		}
		
		// turning

		
		
		
	}
	
	
	
	// tests if a given target is within the gimbal limits of this turret 
	public override bool TargetWithinLimits ( Transform target ) {
		if (error == true) { return false; }
		if (target) {
			// find target's location in rotation plane
			
			return true;
		} else {
			return false;
		}
	}
	
	// check if the turret is aimed at the target
	public bool AimCheck ( float targetSize, float aimTolerance ) {
		if (error == true) { return false; }
		return true;
	}
	
	bool CheckErrors () {
		error = false;
		string _object = gameObject.name;
		if (weaponMount == false) { Debug.Log(_object+": Turret weapon mount part hasn't been defined."); error = true; }
		if (transform.localScale.x != transform.localScale.z) { Debug.Log(_object+" Turret x and z transform scale must be equal."); error = true; }
		
		return error;
	}
}

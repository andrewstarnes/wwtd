using UnityEngine;
using System.Collections;
using UnitScripts;

public class ParticleCollision : MonoBehaviour {

	public float damageToInflict = 1f;
	public float infantryDamageMultiplier = 1f;
	public float tankInfantryMultiplier = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnParticleCollision(GameObject aOther) {
		if(aOther.GetComponent<BasicUnit>()!=null) {
			Debug.Log ("Particle Collision"+aOther.name);
			aOther.GetComponent<BasicUnit>().hitUnit(damageToInflict,infantryDamageMultiplier,tankInfantryMultiplier);
		}
		
	}
}

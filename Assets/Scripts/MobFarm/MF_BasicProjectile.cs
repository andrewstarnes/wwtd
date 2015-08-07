using UnityEngine;
using System.Collections;
using UnitScripts;
using PigeonCoopToolkit.Effects.Trails;

public class MF_BasicProjectile : MonoBehaviour {

	public float damage;
	public float splashDamage;
	public float splashRange;
	public string awaker = "";
	public static EZObjectPool blastObjectPool;
	public string blastObjectPoolName;
	[HideInInspector] public float duration;
	
	public float startTime;
	
	public void Start() {
		if(blastObjectPool==null) {
			blastObjectPool = GameObject.Find(blastObjectPoolName).gameObject.GetComponent<EZObjectPool>();
		}
	}

	public void Awake() {


	
	}

	public virtual void FixedUpdate () {
		if (Time.time >= startTime + duration) {
			this.gameObject.SetActive(false);
		}
		// cast a ray to check hits along path - compensating for fast animation
		RaycastHit hit = default(RaycastHit);
		if ( Physics.Raycast(transform.position, GetComponent<Rigidbody>().velocity, out hit, GetComponent<Rigidbody>().velocity.magnitude * Time.fixedDeltaTime, ~(1<<11) ) ) {
			this.gameObject.SetActive(false);
			blastObjectPool.TryGetNextObject(hit.point,Quaternion.identity);
			DoHit( hit.collider.gameObject );
		}
	}
	public void OnDisable() {
		Trail t = this.GetComponent<Trail>();
		t.ClearSystem(false);
	}
	public void OnEnable() {
		Trail t = this.GetComponent<Trail>();
		t.Emit = true;
	} 
	protected void DoHit ( GameObject thisObject ) {
		// do stuff to the target object when it gets hit
		if ( thisObject.GetComponent<BasicUnit>() ) {
			thisObject.GetComponent<BasicUnit>().health -= damage;
		}
	}
}

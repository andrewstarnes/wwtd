using UnityEngine;
using System.Collections;

public class MF_BasicProjectile : MonoBehaviour {

	public float damage;
	public static EZObjectPool blastObjectPool;
	public string blastObjectPoolName;
	[HideInInspector] public float duration;
	
	public float startTime;
	
	void Start() {
		if(blastObjectPool==null) {
			blastObjectPool = GameObject.Find(blastObjectPoolName).gameObject.GetComponent<EZObjectPool>();
		}
	}
	
	void FixedUpdate () {
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
	
	private void DoHit ( GameObject thisObject ) {
		// do stuff to the target object when it gets hit
		if ( thisObject.GetComponent<MF_BasicStatus>() ) {
			thisObject.GetComponent<MF_BasicStatus>().health -= damage;
		}
	}
}

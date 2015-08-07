using UnityEngine;
using System.Collections;
using UnitScripts;
using PigeonCoopToolkit.Effects.Trails;

public class MF_LaserProjectile : MF_BasicProjectile {

	 
	public bool ending = false;
	public MeshRenderer m;
	public Vector3 firstVelocity;
	public bool isFirst = false;
	public float waitExpiration = 0f;
	public bool waitingToFly = false;
	public void OnDisable() {
		Trail t = this.GetComponent<Trail>();
		if(t!=null) t.ClearSystem(false);

		
		SmokeTrail st = this.GetComponent<SmokeTrail>();
		if(st!=null) {
			st.ClearSystem(false);
		}

	}
	public void OnEnable() {
		if(m==null) {
			m = this.GetComponent<MeshRenderer>();
		}
		ending = false;
		Trail t = this.GetComponent<Trail>();
		if(t!=null) {
			t.Emit = true;
			t.ClearSystem(true);
		}
		SmokeTrail st = this.GetComponent<SmokeTrail>();
		if(st!=null) {
			st.Emit = true;
			st.ClearSystem(true);
		}
		m.enabled = true;
		isFirst = true;

	}

	
	public override void FixedUpdate () {
		float timeNow = Time.time;
		if(isFirst) {
			isFirst = false;
			waitExpiration = timeNow+0.4f;
			this.firstVelocity = this.GetComponent<Rigidbody>().velocity;
			this.GetComponent<Rigidbody>().velocity = firstVelocity/500f;
			waitingToFly = true;
			
			this.GetComponent<Trail>().Update();
			return;
		} else if(timeNow<waitExpiration) {
			
			this.GetComponent<Trail>().Update();
			return;
		} else if(waitingToFly) {
			this.GetComponent<Rigidbody>().velocity = firstVelocity;
			waitingToFly = false;
			this.GetComponent<Trail>().Update();
		}
		if (timeNow >= startTime + duration) {
			Destroy(this.GetComponent<TrailRenderer>());
			this.gameObject.SetActive(false);
		} else {
			if(ending) {
				return;
			}
			
			this.GetComponent<Trail>().Update();
			// cast a ray to check hits along path - compensating for fast animation
			RaycastHit hit = default(RaycastHit);
			if ( Physics.Raycast(transform.position, GetComponent<Rigidbody>().velocity, out hit, GetComponent<Rigidbody>().velocity.magnitude * Time.fixedDeltaTime, ~(1<<11) ) ) {
				blastObjectPool.TryGetNextObject(hit.point,Quaternion.identity);
				if(hit.collider.name=="Terrain") {
					duration += 1f;
					GetComponent<Rigidbody>().velocity = Vector3.zero;
					ending = true;
					m.enabled = false;
				} else
				DoHit( hit.collider.gameObject ); 
			}
		}
	}
	 
} 

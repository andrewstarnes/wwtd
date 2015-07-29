using UnityEngine;
using System.Collections;
using TowerScripts;

public class TowerBase : MonoBehaviour {

	public Transform target;
	public float maxRange = 20f;
	public float rotationSpeed = 0.2f;

	public float fireRate = 1f;
	
	private float _lastFire;
	public NozzleRecoilBase recoiler;
	// Use this for initialization
	void Start () {
		_lastFire = Time.time;
	}
	
	// Update is called once per frame
	private void findTarget() {
		GameObject[] g = GameObject.FindGameObjectsWithTag("Creeps");
		float closest = float.MaxValue;
		GameObject closestObj = null;
		for(int i = 0;i<g.Length;i++) {
			if(Vector3.SqrMagnitude(g[i].transform.position-this.transform.position)<closest) {
				closestObj = g[i];
				closest = Vector3.SqrMagnitude(g[i].transform.position-this.transform.position);
			}
		}
		closest = Mathf.Sqrt(closest);
		if(closest<maxRange) {
			target = closestObj.transform;
		}
	}
	void Update () {
	
		if(target!=null) {
			Vector3 targetDir = target.position - transform.position;
			if(targetDir.magnitude<maxRange) {
				float step = rotationSpeed * Time.deltaTime;
				Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
				Debug.DrawRay(transform.position, newDir, Color.red);
				transform.rotation = Quaternion.LookRotation(newDir);
				if(Time.time-_lastFire>fireRate) {
					_lastFire = Time.time;
					if(recoiler!=null) {
					//	recoiler.recoil();
					}
				}
			} else {
				target = null;
			}
		} else {
			findTarget();
		}


	}

}

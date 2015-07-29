using UnityEngine;
using System.Collections;
using CreepSpawnerPackage;


#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(CreepSpawner))] 
public class CreepSpawnerHandle : Editor {

	// Use this for initialization
	void Start () {
	
	}
	void OnSceneGUI () {
		CreepSpawner myTarget = (CreepSpawner) target;
		float distOfArrow = 2;
		GameObject gameObject = myTarget.gameObject;
		Quaternion q = myTarget.gameObject.transform.rotation;
		Vector3 fwd = Vector3.forward*distOfArrow;
		
		Vector3 arrowEnd = q*fwd;
		DrawArrow.ForDebug(gameObject.transform.position,arrowEnd);
	}
}
#endif


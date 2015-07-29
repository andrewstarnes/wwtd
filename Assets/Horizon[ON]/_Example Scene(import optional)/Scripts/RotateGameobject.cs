using UnityEngine;
using System.Collections;

namespace Horizon {
	public class RotateGameobject : MonoBehaviour {

		public Vector3 rotateSpeed;

		void Update () {
			transform.Rotate (rotateSpeed.x*Time.deltaTime, rotateSpeed.y*Time.deltaTime, rotateSpeed.z*Time.deltaTime);
		}
	}
}
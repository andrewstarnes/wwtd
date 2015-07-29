using UnityEngine;
using System.Collections;

namespace Horizon {
	public class Pan : MonoBehaviour {

		public float sensitivity = 1;
		public float lerpSpeed = 1;
		
		float panTargetLerp;
		
		void Update () {
			panTargetLerp = Mathf.Lerp(panTargetLerp, Input.GetAxis("Mouse X")* sensitivity, lerpSpeed * Time.deltaTime);
			transform.localEulerAngles += new Vector3 (0, panTargetLerp * (Time.deltaTime*100), 0);
		}
	}
}
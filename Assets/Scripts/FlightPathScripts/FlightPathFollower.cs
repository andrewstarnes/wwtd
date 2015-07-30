using UnityEngine;
using System.Collections;

namespace FlightPathManager {
	public class FlightPathFollower : MonoBehaviour {
	
		public FlightPath followThisPath;
		public int currentIndex = -1;
		public FlightPathPoint currentNode;
		public float distThreshold = 3f;

		public float turnSpeed = 30f;
		public float maxBank = 90f;
		public float moveSpeed = 4f;
		public float lastBank = 0f;

		public float altitudeAdjust = 0f;
		public Transform banker;
		// Use this for initialization
		void Start () {
		}
	
		private void GetNextNode() {
			currentIndex++;
			if(currentIndex<followThisPath.path.Length) {
				currentNode = followThisPath.path[currentIndex];	
			} else {
				
			}
		}
		
		// Update is called once per frame
		void Update () {

			followPath();
		}
		protected void followPath() {
			if(currentNode==null||currentNode.transform.position.magnitude==0f||distanceToCurrent<distThreshold) {
				if(currentNode!=null&&currentNode.hasEscapedHere) {
					Destroy(this.gameObject);
				} else
					GetNextNode();
			}
			if(currentNode.transform.position.magnitude!=0f) {
				Debug.DrawLine(transform.position,currentNode.transform.position,Color.cyan);
				
				Vector3 currentTarget = new Vector3(currentNode.transform.position.x,currentNode.transform.position.y+altitudeAdjust,currentNode.transform.position.z);
				Vector3 lookDirection = currentTarget - transform.position;
				Vector3 normalizedLookDirection = lookDirection.normalized;
				float bank = maxBank * -Vector3.Dot(transform.right, normalizedLookDirection);
				if(bank>lastBank) {
					lastBank+=3f;
					if(bank<lastBank) {
						lastBank = bank;
					}
				} else if(bank<lastBank) {
					lastBank-=3f;
					if(bank>lastBank) {
						lastBank = bank;
					}
				}
				Quaternion rot = Quaternion.LookRotation(normalizedLookDirection);
				banker.localRotation = Quaternion.AngleAxis(lastBank, Vector3.forward);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * turnSpeed);
				transform.Translate(new Vector3(0,0,moveSpeed) * Time.deltaTime);
				
				
			}
		}
		float distanceToCurrent {
			get {
				Vector3 currentTarget = new Vector3(currentNode.transform.position.x,currentNode.transform.position.y+altitudeAdjust,currentNode.transform.position.z);
				
				return Vector3.Distance(this.transform.position,currentTarget);
			}
		}
	}
}

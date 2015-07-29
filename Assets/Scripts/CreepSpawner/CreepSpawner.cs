using UnityEngine;
using System.Collections;

namespace CreepSpawnerPackage {
	public class CreepSpawner : MonoBehaviour {
	
	
		public Wave[] waves;
		public Wave currentWave;
		public int nextWave = 0;
		// Use this for initialization
		void Start () {
			nextWave = 0;
		}
		
		// Update is called once per frame
		void Update () {
			if(currentWave==null||currentWave.complete) {
				if(nextWave<waves.Length) {
					currentWave = waves[nextWave];
					nextWave++;
				}
			} else {
				currentWave.processWave();
			}
		}
	}
}
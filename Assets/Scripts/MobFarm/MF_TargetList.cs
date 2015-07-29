using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MF_TargetList : MonoBehaviour {
	
	public Dictionary<int, TargetData> targetList = new Dictionary<int, TargetData>();

	[HideInInspector] public float lastUpdate; // for target choosing timing
	
}


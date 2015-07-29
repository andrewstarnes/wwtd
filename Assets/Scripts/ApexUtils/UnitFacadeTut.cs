using UnityEngine;
using System.Collections;
using Apex;
using Apex.Units;
using Apex.Steering.Components;
using ApexUtils;

public class UnitFacadeTut : MonoBehaviour {

	// Use this for initialization
	private HumanoidSpeedComponent _speeder;
	void Start () {
		ExtendedUnitFacade unit = this.GetUnitFacade<ExtendedUnitFacade>();
		unit.MoveTo(new Vector3(0f,0f,0f),true);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) {
			this.GetUnitFacade<ExtendedUnitFacade>().Run();
		}
		
	}
}

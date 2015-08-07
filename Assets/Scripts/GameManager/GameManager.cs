using UnityEngine;
using System.Collections;
using Google2u;

public class GameManager : MonoBehaviour {

	public GameObject selectedTowerPrefab;
	// Use this for initialization
	public static GameManager REF;

	public int usersCash = 100;
	private GameObject softTower;
	void Start () {
		REF = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void placeTowerAt(float aX,float aY,float aZ) {
		InterfaceManagerUtils.cleanAllInterfaceWindows();
		GameObject terrain = GameObject.Find ("Terrain");
		Terrain t1 = terrain.GetComponent<Terrain>();
		float height = t1.SampleHeight(new Vector3(aX,0f,aZ));
        GameObject nt = (GameObject) Instantiate(selectedTowerPrefab,new Vector3(aX,height,aZ),Quaternion.identity);
		WWTD_Tower t = nt.GetComponent<WWTD_Tower>();
		t.softPlace();
		InterfaceManagerUtils.confirmTowerWindow(t);
	}

	public bool canBuildTower(TowerListRow aData) {
		if(usersCash>=aData._Cost) {
			usersCash -= aData._Cost;
			return true;
		} else {
			return false;
		}
	}
}

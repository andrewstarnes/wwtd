using UnityEngine;
using System.Collections;

public class ConfirmBuyTower : MonoBehaviour {

	// Use this for initialization
	public WWTD_Tower thisTower;
	public static float lastBuyTime = 0;
	public const float minTimesBetweenBuy = 0.1f;
	void Start () {
		GameObject g = this.transform.FindChild("TowerUpgradeArea").gameObject;
		g.transform.localScale = new Vector3(0f,0f,0f);
		iTween.ScaleTo(g,new Vector3(1f,1f,1f),0.5f);
	}
	

	public void initTower(WWTD_Tower aTower) {
		thisTower = aTower;
		if(Time.time-lastBuyTime<minTimesBetweenBuy) {
			Destroy(this.gameObject);
		}
	}
	public void onBuyTower() {
		if(GameManager.REF.canBuildTower(thisTower.rowData)) {
			thisTower.hardPlace();
		
			GridSelector.REF.setBuildableAtPosition(thisTower.transform.position.x,100f,thisTower.transform.position.z,false);
			thisTower = null;
			lastBuyTime = Time.time;
			Destroy(this.gameObject);
		}
	}

	public void OnDestroy() {
		if(thisTower!=null) {
			Destroy(thisTower.gameObject);
			thisTower = null;
		}
	}
	public void onCancelTower() {
		Destroy(thisTower.gameObject);
		Destroy(this.gameObject);
	}
}

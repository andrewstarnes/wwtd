using UnityEngine;
using System.Collections;
using Google2u;

public class WWTD_Tower : MonoBehaviour {

	public int towerTypeID = 0;
	public MeshRenderer towerRange;
	public TowerListRow rowData;
	private MF_BasicScanner scanner;
	// Use this for initialization
	void Start () {

		getComponents();
		setRange();
		drawRange();
	}

	void getComponents() {
		scanner = this.GetComponent<MF_BasicScanner>();

		for(int i= 0;i<TowerList.Instance.Rows.Count;i++) {
			if(TowerList.Instance.Rows[i]._ID==towerTypeID) {
				
				rowData = TowerList.Instance.Rows[i];
			}
		}
	}

	void setRange() {
		scanner.detectorRange = rowData._Range;
	}
	public void softPlace() {
		
		Camera.main.GetComponent<SimpleDrag>().autoScrollToTower(this.gameObject);
		if(scanner==null) {
			getComponents();
		}
		scanner.enabled = false;
	}
	public void hardPlace() {
		hideRange();
		setWeapons();
		scanner.enabled = true;
	}
	public void setWeapons() {
		MF_FireWeapon[] weapons = this.GetComponentsInChildren<MF_FireWeapon>();
		for(int i = 0;i<weapons.Length;i++) {
			weapons[i].initWeapon(rowData);
		}
		MF_BasicWeapon[] basicWeapons = this.GetComponentsInChildren<MF_BasicWeapon>();
		for(int i = 0;i<basicWeapons.Length;i++) {
			basicWeapons[i].initTower(rowData); 
		}
		MF_ElectroWeapon[] electroWeapons = this.GetComponentsInChildren<MF_ElectroWeapon>();
		for(int i = 0;i<electroWeapons.Length;i++) {
			electroWeapons[i].initWeapon(rowData);
		}
	}
	public void hideRange() {
		if(this.transform.FindChild("Range")!=null) {
			Destroy(this.transform.FindChild("Range").gameObject);
		}
	}
	void drawRange() {
		if(this.transform.FindChild("Range")==null) {
			GameObject g = Instantiate(Resources.Load("WWTD/Utils/TowerRange")) as GameObject;
			g.transform.parent = this.gameObject.transform;
			g.name = "Range";
		}
		towerRange = this.transform.FindChild("Range").GetComponent<MeshRenderer>();
		towerRange.transform.localPosition = new Vector3(0f,1.2f,0f);

		
		while(towerRange.bounds.extents.x<this.scanner.detectorRange) {
			Vector3 scale = towerRange.transform.localScale;
			scale.x += 0.1f;
			scale.z += 0.1f; 
			towerRange.transform.localScale = scale;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}

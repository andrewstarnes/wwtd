using UnityEngine;
using System.Collections;

public class GridSelector : MonoBehaviour {

	public Camera mouseCamera;
	private GridOverlay overlay;
	public GameObject selectedTowerPrefab;

	public GameObject towerBeingPlaced;
	
	public static GridSelector REF;
	public Vector3 lastMousePoint = new Vector3(0f,0f,0f);
	// Use this for initialization
	void Start () {
		overlay = this.GetComponent<GridOverlay>();
		REF = this;
	}
	
	void OnEnable() {
		
		Lean.LeanTouch.OnFingerTap += OnFingerTap;
		Lean.LeanTouch.OnFingerHeldDown += OnHeldDown;
	}
	void OnDisable() {
		
		Lean.LeanTouch.OnFingerDown -= OnFingerTap;
		Lean.LeanTouch.OnFingerHeldDown -= OnHeldDown;
	}
	// Update is called once per frame
	void Update () {
		Ray ray = mouseCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit rh;;

		Debug.DrawRay(ray.origin,ray.direction,Color.red);
		if(Physics.Raycast(ray.origin,ray.direction,out rh,mouseCamera.transform.position.y*2f)) {
		
			Vector3 p = rh.point;
			p.x = p.x / (overlay.cellSize);
			p.z = p.z / (overlay.cellSize);
			p.x = Mathf.FloorToInt(p.x);
			p.z = Mathf.FloorToInt(p.z);
	//		Debug.Log (p);
			if(Input.GetMouseButtonDown(1)) {
				overlay.toggleBuildable((int) p.x,(int) p.z);
			} else if(Input.GetMouseButtonDown(0)) {
				
			}
			lastMousePoint.x = p.x;
			lastMousePoint.z = p.z;
		}
	}
	public void OnFingerTap(Lean.LeanFinger finger)
	{
		// Does the prefab exist?
		if (selectedTowerPrefab != null)
		{
			// Make sure the finger isn't over any GUI elements
			if (finger.IsOverGui == false)
			{
				// Soft Build this tower, show it's range and stuff but don't let it shoot until we've held down for a second.
				if(towerBeingPlaced!=null) {
					Destroy(towerBeingPlaced);
					towerBeingPlaced = null;
				}
				GameObject b = buildTower((int) lastMousePoint.x,(int) lastMousePoint.z);
				if(b!=null) {
					WWTD_Tower tower = b.GetComponent<WWTD_Tower>();
					tower.softPlace();
					towerBeingPlaced = tower.gameObject;
				}
			}
		}
	}
	public void OnHeldDown(Lean.LeanFinger finger)
	{
		// Does the prefab exist?
		if (selectedTowerPrefab != null)
		{
			// Make sure the finger isn't over any GUI elements
			if (finger.IsOverGui == false)
			{
				// Clone the prefab, and place it where the finger was tapped
				if(towerBeingPlaced!=null) {
					towerBeingPlaced.GetComponent<WWTD_Tower>().hardPlace();
					towerBeingPlaced = null;
				}
			}
		}
	}
	public void drawOverlay(bool aShowOverlay) {
		overlay.gameObject.SetActive(aShowOverlay);
	}
	public void setBuildableAtPosition(float aX,float aY,float aZ,bool aNewValue) {

		Vector3 origin = new Vector3(aX,aY,aZ);
		RaycastHit rh;
		if(Physics.Raycast(origin,Vector3.down,out rh,100000f)) {
			
			Vector3 p = rh.point;
			p.x = p.x / (overlay.cellSize);
			p.z = p.z / (overlay.cellSize);
			p.x = Mathf.FloorToInt(p.x);
			p.z = Mathf.FloorToInt(p.z);
			overlay.buildable[(int) p.z*overlay.gridWidth+(int) p.x] = aNewValue;
			overlay.UpdateCells();
			lastMousePoint.x = p.x;
			lastMousePoint.z = p.z;
		}	
	}
	public GameObject buildTower(int aSquareX,int aSquareZ) {
		float xPos = (float) aSquareX*overlay.cellSize+(overlay.cellSize/2);
		float zPos = (float) aSquareZ*overlay.cellSize+(overlay.cellSize/2);
		if(overlay.buildable[aSquareZ*overlay.gridWidth+aSquareX]) {
			GameManager.REF.placeTowerAt(xPos,overlay.heights[aSquareZ+overlay.gridWidth+aSquareX]-1f,zPos);

		}
		return null;

	}
}

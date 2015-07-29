using UnityEngine;
using System.Collections;

public class GridSelector : MonoBehaviour {

	public Camera mouseCamera;
	private GridOverlay overlay;
	public GameObject selectedTowerPrefab;


	public Vector3 lastMousePoint = new Vector3(0f,0f,0f);
	// Use this for initialization
	void Start () {
		overlay = this.GetComponent<GridOverlay>();
	}
	
	void OnEnable() {
		
		Lean.LeanTouch.OnFingerTap += OnFingerTap;
	}
	void OnDisable() {
		
		Lean.LeanTouch.OnFingerTap -= OnFingerTap;
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
			//	overlay.toggleBuildable((int) p.x,(int) p.z);
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
				// Clone the prefab, and place it where the finger was tapped
				
				buildTower((int) lastMousePoint.x,(int) lastMousePoint.z);
			}
		}
	}
	public bool buildTower(int aSquareX,int aSquareZ) {
		float xPos = (float) aSquareX*overlay.cellSize+(overlay.cellSize/2);
		float zPos = (float) aSquareZ*overlay.cellSize+(overlay.cellSize/2);
		if(overlay.buildable[aSquareZ*overlay.gridWidth+aSquareX]) {
			GameObject nt = (GameObject) Instantiate(selectedTowerPrefab,new Vector3(xPos,overlay.heights[aSquareZ+overlay.gridWidth+aSquareX],zPos),Quaternion.identity);
			overlay.buildable[aSquareZ*overlay.gridWidth+aSquareX] = false;
			overlay.UpdateCells();
			return true;
		}
		return false;
		

	}
}

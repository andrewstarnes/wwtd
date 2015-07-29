using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridOverlay : MonoBehaviour {
	public float cellSize = 1;
	public int gridWidth = 10;
	public int gridHeight = 10;
	public float yOffset = 0.5f;
	public Material cellMaterialValid;
	public Material cellMaterialInvalid; 
	
	private GameObject[] _cells;
	public float[] heights;
	
	public bool[] buildable;
	void Start() {
		_cells = new GameObject[gridHeight * gridWidth];
		if(buildable==null||buildable.Length==0) {
			buildable = new bool[gridHeight*gridWidth];
			for(int i = 0;i<buildable.Length;i++) {
				buildable[i] = false;
			}
		}
		heights = new float[(gridHeight + 1) * (gridWidth + 1)];
		
		for (int z = 0; z < gridHeight; z++) {
			for (int x = 0; x < gridWidth; x++) {
				if(buildable[z * gridWidth + x])
					_cells[z * gridWidth + x] = CreateChild();
			}
		}
		
		UpdateHeights();
		UpdateCells();
	}
	public void toggleBuildable(int aX,int aZ) {
		bool newBuildableState = !buildable[aZ * gridWidth + aX];
		buildable[aZ * gridWidth + aX] = newBuildableState;
		if(_cells[aZ*gridWidth+aX]!=null&&newBuildableState==false) {
			Destroy(_cells[aZ*gridWidth+aX]);
		} else if(_cells[aZ*gridWidth+aX]==null&&newBuildableState==true) {
			_cells[aZ * gridWidth + aX] = CreateChild();

		}
		UpdateSize();
		UpdateHeights();
		UpdateCells();
	}
	void Update () {
	//	UpdateSize();
	//	UpdatePosition();
	//	UpdateHeights();
	//	UpdateCells();
	}
	
	GameObject CreateChild() {
		GameObject go = new GameObject();
		
		go.name = "Grid Cell";
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		go.AddComponent<MeshRenderer>();
		go.AddComponent<MeshFilter>().mesh = CreateMesh();

		MeshFilter g = go.GetComponent<MeshFilter>();
		Vector3 extremeBound = new Vector3(256f,256f,256f);
		MeshRenderer r = go.GetComponent<MeshRenderer>();
		r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		r.receiveShadows = false;
		g.sharedMesh.bounds = new Bounds (new Vector3(-128f,-128f,128f),extremeBound);
		return go;
	}
	
	public void UpdateSize() {
		int newSize = gridHeight * gridWidth;
		int oldSize = _cells.Length;
		
		if (newSize == oldSize)
			return;
		
		GameObject[] oldCells = _cells;
		_cells = new GameObject[newSize];
		
		if (newSize < oldSize) {
			for (int i = 0; i < newSize; i++) {
				_cells[i] = oldCells[i];
			}
			
			for (int i = newSize; i < oldSize; i++) {
				Destroy(oldCells[i]);
			}
		}
		else if (newSize > oldSize) {
			for (int i = 0; i < oldSize; i++) {
				_cells[i] = oldCells[i];
			}
			
			for (int i = oldSize; i < newSize; i++) {
				_cells[i] = CreateChild();
			}
		}
		
		heights = new float[(gridHeight + 1) * (gridWidth + 1)];
	}
	
	void UpdatePosition() {
		RaycastHit hitInfo;
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));
		Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain"));
		Vector3 position = hitInfo.point;
		
		position.x -= hitInfo.point.x % cellSize + gridWidth * cellSize / 2;
		position.z -= hitInfo.point.z % cellSize + gridHeight * cellSize / 2;
		position.y = 0;
		
		transform.position = position;
	}
	
	public void UpdateHeights() {
		RaycastHit hitInfo;
		Vector3 origin;
		
		for (int z = 0; z < gridHeight + 1; z++) {
			for (int x = 0; x < gridWidth + 1; x++) {
				origin = new Vector3(x * cellSize, 200, z * cellSize);
				Physics.Raycast(transform.TransformPoint(origin), Vector3.down, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain"));
				
				heights[z * (gridWidth + 1) + x] = hitInfo.point.y;
			}
		}
	}
	
	public void UpdateCells() {
		for (int z = 0; z < gridHeight; z++) {
			for (int x = 0; x < gridWidth; x++) {
				GameObject cell = _cells[z * gridWidth + x];
				if(cell!=null&&buildable[z*gridWidth+x]) {
					MeshRenderer meshRenderer = cell.GetComponent<MeshRenderer>();
					MeshFilter meshFilter = cell.GetComponent<MeshFilter>();
					
					meshRenderer.material = IsCellValid(x, z) ? cellMaterialValid : cellMaterialInvalid;
					UpdateMesh(meshFilter.mesh, x, z);
				} else if(!buildable[z*gridWidth+x]&&cell!=null) {
					Destroy(_cells[z*gridWidth+x]);
				}
			}
		}
	}
	
	bool IsCellValid(int x, int z) {
		RaycastHit hitInfo;
		Vector3 origin = new Vector3(x * cellSize + cellSize/2, 200, z * cellSize + cellSize/2);
		Physics.Raycast(transform.TransformPoint(origin), Vector3.down, out hitInfo, Mathf.Infinity, LayerMask.GetMask("Buildings"));
		
		return hitInfo.collider == null;
	}
	
	Mesh CreateMesh() {
		Mesh mesh = new Mesh();
		
		mesh.name = "Grid Cell";
		mesh.vertices = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
		mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
		mesh.normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up, Vector3.up };
		mesh.uv = new Vector2[] { new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 0) };
		
		return mesh;
	}
	
	void UpdateMesh(Mesh mesh, int x, int z) {
		mesh.vertices = new Vector3[] {
			MeshVertex(x, z),
			MeshVertex(x, z + 1),
			MeshVertex(x + 1, z),
			MeshVertex(x + 1, z + 1),
		};
	}
	
	Vector3 MeshVertex(int x, int z) {
		return new Vector3(x * cellSize, heights[z * (gridWidth + 1) + x] + yOffset, z * cellSize);
	}
}
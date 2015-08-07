using UnityEngine;

// This script allows you to drag this GameObject using any finger, as long it has a collider
public class SimpleDrag : MonoBehaviour
{
	// This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)
	public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;
	// This stores the finger that's currently dragging this GameObject
	private Lean.LeanFinger draggingFinger;
	
	public GameObject moveThisObjectInstead;

	public Vector3 moveToHere;
	const float towerViewOffset = 20f;
	protected virtual void OnEnable()
	{
		// Hook into the OnFingerDown event
		Lean.LeanTouch.OnFingerDown += OnFingerDown;
		
		// Hook into the OnFingerUp event
		Lean.LeanTouch.OnFingerUp += OnFingerUp;
	}
	
	protected virtual void OnDisable()
	{
		// Unhook the OnFingerDown event
		Lean.LeanTouch.OnFingerDown -= OnFingerDown;
		
		// Unhook the OnFingerUp event
		Lean.LeanTouch.OnFingerUp -= OnFingerUp;
	}
	
	public void autoScrollToTower(GameObject aTower) {
		Vector3 pos = aTower.gameObject.transform.position;
		moveToHere = new Vector3(pos.x,this.transform.position.y,pos.z-towerViewOffset);
	}
	protected virtual void LateUpdate()
	{
		// If there is an active finger, move this GameObject based on it
		if (draggingFinger != null)
		{
			moveToHere = new Vector3();
			Camera.main.GetComponent<SimpleDrag>().moveToHere = new Vector3(0f,0f,0f);
			Lean.LeanTouch.MoveObject(moveThisObjectInstead.gameObject.transform, draggingFinger.DeltaScreenPosition);
		} else if(moveToHere.magnitude>0f) {
			float step = 20f * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, moveToHere, step);
		}
	}
	
	public void OnFingerDown(Lean.LeanFinger finger)
	{
		// Raycast information
		var ray = finger.GetRay();
		var hit = default(RaycastHit);
		
		// Was this finger pressed down on a collider?
		if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
		{
			// Was that collider this one?
			if (hit.collider.gameObject == this.gameObject)
			{
				// Set the current finger to this one
				draggingFinger = finger;
			}
		}
	}
	
	public void OnFingerUp(Lean.LeanFinger finger)
	{
		// Was the current finger lifted from the screen?
		if (finger == draggingFinger)
		{
			// Unset the current finger
			draggingFinger = null;
		}
	}
}
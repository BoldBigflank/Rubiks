using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {
	bool locked;
	
	// Use this for initialization
	void Start () {
		locked = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
//	public void Rotate(Vector3 axis, float angle){
//		transform.RotateAround(Vector3.zero, axis, angle);
//	}
	
	public void PointerDown(bool down){
		if(down){
			locked = true;
		} else {
			// Snap back and unlock
			Vector3 newRotation = transform.rotation.eulerAngles;
			newRotation.x = Mathf.Round(newRotation.x/90.0F) * 90.0F;
			newRotation.y = Mathf.Round(newRotation.y/90.0F) * 90.0F;
			newRotation.z = Mathf.Round(newRotation.z/90.0F) * 90.0F;
			transform.rotation = Quaternion.Euler(newRotation);
			locked = false;
			// Remove all children
//			for(int i = 0; i < transform.childCount; i++){
//				transform.GetChild(i).parent = null;
//			}
			while(transform.childCount > 0){
				transform.GetChild(0).parent = transform.parent;
			}		
		}
		
	}
	
	
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube : MonoBehaviour {
	public static RubiksCube current;
	public GameObject cube;
	public GameObject mainCamera;
	public GameObject spinner;
	
	const int COLUMNS = 3;
	List<GameObject> cubes;
	bool pointerDown;
	Vector3 focusPoint; // The angle the camera is
	Vector3 oldPoint;
	Vector3 focusFace; // The transform of the clicked face
	Vector3 rotateAxis;
	
	// Use this for initialization
	void Start () {
		current = this;
		pointerDown = false;
		rotateAxis = Vector3.zero;
		cubes = new List<GameObject>();
		
		// Make a cube for every spot
		for(int i = 0; i < COLUMNS; i++){
			for(int j = 0; j < COLUMNS; j++){
				for(int k = 0; k < COLUMNS; k++){
					// Make sure it's on a side
					if(i == 0 || i == COLUMNS-1 || j == 0 || j == COLUMNS-1 || k == 0 || k == COLUMNS-1){
						GameObject c = (GameObject)Instantiate(cube, Vector3.zero, Quaternion.identity);
						c.name = ("Cube" + i + "," + j + "," + k);
						c.transform.parent = transform;
						c.GetComponent<Cube>().Initialize(i-1,j-1,k-1);
						cubes.Add (c);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(Vector3.zero, focusPoint, Color.red);
		Debug.DrawRay (Vector3.zero, focusFace, Color.green);
		if(pointerDown){
			Vector3 newPoint = mainCamera.transform.forward;
			float angle = Vector3.Angle(focusPoint, newPoint);
			if(rotateAxis == Vector3.zero){
				if(angle > 10.0f){
					// Set the axis
					SetAxis (Vector3.Cross(focusPoint, newPoint));
					// Add relevant children cubes to the spinner
					foreach(GameObject c in cubes){
						if(rotateAxis == Vector3.right){
							if(Mathf.RoundToInt(c.transform.position.x) == Mathf.RoundToInt(focusFace.x)){
								c.transform.parent = spinner.transform;
							}
						} else if(rotateAxis == Vector3.up){
							if(Mathf.RoundToInt(c.transform.position.y) == Mathf.RoundToInt(focusFace.y)){
								c.transform.parent = spinner.transform;
							}
						} else if(rotateAxis == Vector3.forward){
							if(Mathf.RoundToInt(c.transform.position.z) == Mathf.RoundToInt(focusFace.z)){
								c.transform.parent = spinner.transform;
							}
						}
						
					}
				}
			} else {
				// Set the spinner based on the two points
				float rotateAngle = Vector3.Angle(oldPoint, newPoint);
				Vector3 cross = Vector3.Cross (oldPoint, newPoint);
				cross = Vector3.Scale(cross, rotateAxis);
				if(cross.x + cross.y + cross.z < 0.0F){
					rotateAngle *= -1.0f;
				}
				Debug.Log("Cross: " + cross);
//				 if(cross, rotateAxis)) < 0){
//					rotateAngle *= -1.0F;
//				}
				Debug.Log ("rotateAngle: " + rotateAngle);
				spinner.transform.RotateAround(Vector3.zero, rotateAxis, rotateAngle);
			}
			
			oldPoint = newPoint;
		}
	}
	
	public void SetFocus(Vector3 point){
//		Debug.Log ("Setting focus" + point);
		if(!pointerDown){
			focusFace = point;
		}
//		if(pointerDown && rotateAxis == Vector3.zero){
//			Vector3 cross = Vector3.Cross(focusPoint, point);
//			float max = Mathf.Max(Mathf.Abs (cross.x), Mathf.Abs (cross.y), Mathf.Abs (cross.z)  );
//			if(Mathf.Abs(cross.x) == max ){
//				// X axis
//				rotateAxis = Vector3.right;
//			}
//			
//			// Get all the cubes of the column that includes the focusPoint
//		}
//		focusPoint = point;
	}
	
//	public bool isClickedDown(){
//		return pointerDown;
//	}

	public void PointerDown(bool down){
		Debug.Log("Rubik's Cube PointerDown: " + down);
		if(!pointerDown && down){
			rotateAxis = Vector3.zero;
			focusPoint = mainCamera.transform.forward;
			
			spinner.GetComponent<Spinner>().PointerDown(down);
		}
		if(!down && pointerDown){ // Releasing
		
			spinner.GetComponent<Spinner>().PointerDown(down);
		}
		pointerDown = down;
	}
	
	void SetAxis(Vector3 cross){
		float max = Mathf.Max(Mathf.Abs (cross.x), Mathf.Abs (cross.y), Mathf.Abs (cross.z)  );
		if(Mathf.Abs(cross.x) == max ){
			rotateAxis = Vector3.right;
		} else if(Mathf.Abs (cross.y) == max){
			rotateAxis = Vector3.up;
		} else if (Mathf.Abs (cross.z) == max) {
			rotateAxis = Vector3.forward;
		}
	}
}

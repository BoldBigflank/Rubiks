using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube : MonoBehaviour {
	public static RubiksCube current;
	public GameObject cube;
	const int COLUMNS = 3;
	List<GameObject> cubes;
	bool pointerDown;
	Vector3 focusPoint;
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
	
	}
	
	public void SetFocus(Vector3 point){
		Debug.Log ("Setting focus" + point);
		
		if(pointerDown && rotateAxis == Vector3.zero){
			Vector3 cross = Vector3.Cross(focusPoint, point);
			float max = Mathf.Max(Mathf.Abs (cross.x), Mathf.Abs (cross.y), Mathf.Abs (cross.z)  );
			if(Mathf.Abs(cross.x) == max ){
				// X axis
				rotateAxis = Vector3.right;
			}
			
			// Get all the cubes of the column that includes the focusPoint
		}
		focusPoint = point;
	}
	
	public bool isClickedDown(){
		return pointerDown;
	}
	public void PointerDown(bool down){
		Debug.Log("Rubik's Cube PointerDown");
		pointerDown = down;
		if(!pointerDown){
			rotateAxis = Vector3.zero;
		}
	}
}

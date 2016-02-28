using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RubiksCube : MonoBehaviour {
	public static RubiksCube current;
	public GameObject cube;
	public GameObject mainCamera;
	public GameObject spinner;

	// Game Variables
	bool solved;
	float timer;
	int turns;

	// UI
	public Text timerText;
	public Text turnsText;
	
	const int COLUMNS = 3;
	List<GameObject> cubes;
	List<float> neighborDistance;
	bool pointerDown;
	Vector3 focusPoint; // The angle the camera is
	Vector3 oldPoint; // The previous frame's angle
	Vector3 focusFace; // The transform of the clicked face
	Vector3 rotateAxis;
	
	// Use this for initialization
	void Start () {
		RenderSettings.ambientLight = Color.white;
		current = this;
		pointerDown = false;
		rotateAxis = Vector3.zero;
		cubes = new List<GameObject>();
		neighborDistance = new List<float>();
		timer = 0.0f;
		turns = 0;
		solved = false;
		
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
		// Make the solution
		for(int i = 0; i<cubes.Count; i++){
			Vector3 cube = cubes[i].transform.position;
			Vector3 prevCube = cubes[(i-1+cubes.Count)%cubes.Count].transform.position;
			neighborDistance.Add(Vector3.Distance(cube, prevCube));
		}
		NewGame();
	}
	
	bool CheckWin(){
		// Go through array of cubes
		// Make sure the previous one is just as far as when it was instantiated
		for(int i = 0; i<cubes.Count; i++){
			Vector3 cube = cubes[i].transform.position;
			Vector3 prevCube = cubes[(i-1+cubes.Count)%cubes.Count].transform.position;
			if(Mathf.Abs( Vector3.Distance(cube, prevCube) - neighborDistance[i]) > 0.1F) {
				Debug.Log ("You didn't win: " + (Vector3.Distance(cube, prevCube) - neighborDistance[i]));
				return false;
			}
		}
		solved = true;
		Debug.Log ("YOU WON!!!!!!!!");
		return true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!solved) timer += Time.deltaTime;
		// Update the UI
		timerText.text = "Timer: " + ((int)(timer / 60)).ToString("0") + ":" + ((int)(timer % 60)).ToString("00");
		turnsText.text = "Turns: " + turns;

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
				spinner.transform.RotateAround(Vector3.zero, rotateAxis, rotateAngle);
			}
			
			oldPoint = newPoint;
		}
	}
	
	public void Randomize(int turns){
		for(int i = 0; i < turns; i++){
			// Choose an axis
			Vector3 randomAxis = Vector3.zero;
			int randomAxisInt = Random.Range(0,3);
			
			if(randomAxisInt == 0) randomAxis = Vector3.right;
			if(randomAxisInt == 1) randomAxis = Vector3.up;
			if(randomAxisInt == 2) randomAxis = Vector3.forward;
			
			// Choose a cube
			int cubeNumber = Random.Range(0, cubes.Count);
			GameObject cube = cubes[cubeNumber];
			
			// Choose an angle 90,180,270
			float randomAngle = Random.Range(1,4) * 90.0F;
			
			RotateColumn(randomAxis, cube.transform.position, randomAngle);
		}
	}
	
	public void RotateColumn(Vector3 axis, Vector3 focusFace, float angle){
		foreach(GameObject c in cubes){
			if(axis == Vector3.right){
				if(Mathf.RoundToInt(c.transform.position.x) == Mathf.RoundToInt(focusFace.x)){
					c.transform.RotateAround(Vector3.zero, axis, angle);
				}
			} else if(axis == Vector3.up){
				if(Mathf.RoundToInt(c.transform.position.y) == Mathf.RoundToInt(focusFace.y)){
					c.transform.RotateAround(Vector3.zero, axis, angle);
				}
			} else if(axis == Vector3.forward){
				if(Mathf.RoundToInt(c.transform.position.z) == Mathf.RoundToInt(focusFace.z)){
					c.transform.RotateAround(Vector3.zero, axis, angle);
				}
			}
			
		}
	}
	
	public void SetFocus(Vector3 point){
		if(!pointerDown){
			focusFace = point;
		}
	}
	
	public void PointerDown(bool down){
		Debug.Log("Rubik's Cube PointerDown: " + down);
		if(!pointerDown && down){
			rotateAxis = Vector3.zero;
			focusPoint = mainCamera.transform.forward;
			
			spinner.GetComponent<Spinner>().PointerDown(down);
		}
		if(!down && pointerDown){ // Releasing
			spinner.GetComponent<Spinner>().PointerDown(down);
			CheckWin();
			if(!solved && rotateAxis != Vector3.zero) turns++;
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
	
	public void NewGame(){
		// Reset the timer/turns
		timer = 0.0f;
		turns = 0;
		solved = false;

		foreach(GameObject c in cubes){
			c.GetComponent<Cube>().Reset();
		}
		Randomize (20);
	}
}

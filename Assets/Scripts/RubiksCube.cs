using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube : MonoBehaviour {
	public GameObject cube;
	const int COLUMNS = 3;
	List<GameObject> cubes;
	
	// Use this for initialization
	void Start () {
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
}

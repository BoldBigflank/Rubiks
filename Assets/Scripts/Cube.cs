using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Cube : MonoBehaviour {
	int x;
	int y;
	int z;
	public GameObject face;
	public Material[] colors;
	List<GameObject> faces;
	
	// Use this for initialization
	void Awake () {
		faces = new List<GameObject>();
		List<Vector3> positions = new List<Vector3>();
		positions.Add( new Vector3(0.5F,0.0F,0.0F));
		positions.Add( new Vector3(-0.5F,0.0F,0.0F));
		positions.Add( new Vector3(0.0F,0.5F,0.0F));
		positions.Add( new Vector3(0.0F,-0.5F,0.0F));
		positions.Add( new Vector3(0.0F,0.0F,0.5F));
		positions.Add( new Vector3(0.0F,0.0F,-0.5F));
		// Make six faces
//		foreach(Vector3 position in positions){
		for(int i = 0; i < positions.Count; i++){
			Vector3 position = positions[i];
			GameObject g = Instantiate (face, position, Quaternion.identity) as GameObject;
			g.transform.LookAt(2.0F * position, Vector3.up);
			g.transform.parent = transform;
			g.GetComponent<Renderer>().material = colors[i];
			faces.Add(g);
		}
	}
	
	// Update is called once per frame
	void Update () {
//		Vector3 localPosition = transform.localPosition;
//		localPosition.x = Mathf.RoundToInt(localPosition.x);
//		localPosition.y = Mathf.RoundToInt(localPosition.y);
//		localPosition.z = Mathf.RoundToInt(localPosition.z);
//		transform.localPosition = localPosition;
	}
	
	public void Reset(){
		transform.localRotation = Quaternion.identity;
		transform.localPosition = new Vector3(x, y, z);
	}
	
	public void Initialize(int x, int y, int z){
		this.x = x;
		this.y = y;
		this.z = z;
		gameObject.transform.localScale = Vector3.one;
		transform.localPosition = new Vector3(x, y, z);
		// Turn off faces that are not on the edge
		foreach(GameObject face in faces){
			if(x > 0 && face.transform.localPosition.x > 0){
				face.SetActive(true);
			}
			if(x < 0 && face.transform.localPosition.x < 0){
				face.SetActive(true);
			}
			if(y > 0 && face.transform.localPosition.y > 0){
				face.SetActive(true);
			}
			if(y < 0 && face.transform.localPosition.y < 0){
				face.SetActive(true);
			}
			if(z > 0 && face.transform.localPosition.z > 0){
				face.SetActive(true);
			}
			if(z < 0 && face.transform.localPosition.z < 0){
				face.SetActive(true);
			}
			
		}
		
	}

	
}

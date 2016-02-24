using UnityEngine;
using System.Collections;

public class Face : MonoBehaviour {
	Color startColor;
	Color highlightColor;
	
	// Use this for initialization
	void Start () {
		startColor = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
		highlightColor = new Color(startColor.r + 0.5f, startColor.g + 0.5f, startColor.b + 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void GazeEnter(bool gazedAt){
		GetComponent<Renderer>().material.color = gazedAt ? highlightColor: startColor;
	}
	
	
}

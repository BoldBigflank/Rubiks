using UnityEngine;
using System.Collections;

public class CanvasFocus : MonoBehaviour {
	Vector3 startScale;
	// Use this for initialization
	void Start () {
		startScale = gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PointerFocus(bool focus){
		gameObject.transform.localScale = (focus) ? startScale * 2.0f : startScale;

	}
}

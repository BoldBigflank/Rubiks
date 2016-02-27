using UnityEngine;
using System.Collections;

public class Swirly : MonoBehaviour {
	Vector3 randomAxis;
	public float speed = 20.0f;
	float xSpeed;
	float ySpeed;
	float zSpeed;
	
	// Use this for initialization
	void Start () {
		xSpeed = Random.Range (40.0f, 80.0f);
		ySpeed = Random.Range (40.0f, 80.0f);
		zSpeed = Random.Range (40.0f, 80.0f);
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Vector3.zero, Vector3.right, xSpeed * speed * Time.deltaTime);
		transform.RotateAround(Vector3.zero, Vector3.up, ySpeed * speed * Time.deltaTime);
		transform.RotateAround(Vector3.zero, Vector3.forward, zSpeed * speed * Time.deltaTime);
		
	}
}

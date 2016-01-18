using UnityEngine;
using System.Collections;

public class ShipThrust : MonoBehaviour {

	public float speed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var offset = Vector3.forward * Time.deltaTime * speed;
		this.transform.Translate(offset);
	}
}

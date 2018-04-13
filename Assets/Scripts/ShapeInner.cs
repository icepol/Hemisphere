using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeInner : MonoBehaviour {

	Quaternion quaternion;

	// Use this for initialization
	void Start () {
		quaternion = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = quaternion;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWrapper : MonoBehaviour {

	float rotation;

	void Start() {
		StartCoroutine (InitRotations ());
	}

	public void SetRotation(float rotation) {
		this.rotation = rotation;
	}
		
	IEnumerator InitRotations() {
		yield return null;

		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
	}
}

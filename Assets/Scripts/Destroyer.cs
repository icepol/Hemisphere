using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		MovingShape movingShape = collider.gameObject.GetComponent<MovingShape> ();
		if (movingShape) {
			Destroy (collider.gameObject);
		}
	}
}

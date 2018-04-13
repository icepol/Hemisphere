using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
		spriteRenderer = gameObject.GetComponentInChildren<ShapeInner>().GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		ColorSphere colorSphere = col.gameObject.GetComponent<ColorSphere> ();
		if (colorSphere) {
			ColorSphereIn (colorSphere);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		ColorSphere colorSphere = col.gameObject.GetComponent<ColorSphere> ();
		if (colorSphere) {
			ColorSphereOut (colorSphere);
		}
	}

	void ColorSphereIn(ColorSphere colorSphere) {
		spriteRenderer.color = colorSphere.GetComponent<SpriteRenderer> ().color;
	}

	void ColorSphereOut(ColorSphere colorSphere) {
		spriteRenderer.color = Color.black;
	}

	public void SetShape(Sprite shape) {
		spriteRenderer.sprite = shape;
	}

	public bool Match(MovingShape movingShape) {
		return movingShape.Color == spriteRenderer.color && movingShape.Shape == spriteRenderer.sprite;
	}
}

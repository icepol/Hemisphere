using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSphere : MonoBehaviour {

	SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = gameObject.GetComponentInChildren<ShapeInner>().GetComponent<SpriteRenderer>();
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
        spriteRenderer.color = colorSphere.Color;
	}

	void ColorSphereOut(ColorSphere colorSphere) {
		spriteRenderer.color = Color.black;
	}

	public void SetShape(Sprite shape) {
		spriteRenderer.sprite = shape;
	}
}

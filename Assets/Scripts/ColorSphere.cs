using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSphere : MonoBehaviour {

    SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = gameObject.GetComponentInChildren<ShapeInner>().GetComponent<SpriteRenderer>();
    }

    public Color Color {
        get {
            return spriteRenderer.color;
        }

        set {
            spriteRenderer.color = value;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchResult {
    Ok,
    Wrong,
    Ignore,
};

public class StaticShape : MonoBehaviour {
    
    SpriteRenderer spriteRenderer;
    ShapeSphere shapeSphere;
    ColorSphere colorSphere;

    void Awake() {
        spriteRenderer = GetComponentInChildren<ShapeInner>().GetComponent<SpriteRenderer>();
        shapeSphere = GetComponent<ShapeSphere>();
        colorSphere = GetComponent<ColorSphere>();
    }

    bool IsColorMatch(MovingShape movingShape) {
        return movingShape.Color == spriteRenderer.color;
    }

    bool IsShapeMatch(MovingShape movingShape) {
        return movingShape.Shape == spriteRenderer.sprite;
    }

    public MatchResult Match(MovingShape movingShape) {
        if (movingShape.IsColorSet() && movingShape.IsShapeSet()) {
            // both must be set
            return IsColorMatch(movingShape) && IsShapeMatch(movingShape) 
                ? MatchResult.Ok : MatchResult.Wrong;
        }

        if (movingShape.IsColorSet()) {
            // only color must match
            return movingShape.Color == spriteRenderer.color
                ? MatchResult.Ok : MatchResult.Ignore;
        }
        
        if (movingShape.IsShapeSet()) {
            // only color must match
            return movingShape.Shape == spriteRenderer.sprite
                ? MatchResult.Ok : MatchResult.Ignore;
        }

        return MatchResult.Ignore;
    }
}

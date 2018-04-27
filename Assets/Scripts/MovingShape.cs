using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShape : MonoBehaviour {

	static int count = 0;

	float time;
	float currentTime = 0;
	bool isPaused = false;
    bool isActive = true;

    bool colorSet = false;
    bool shapeSet = false;

	Vector3 startPosition;
	Vector3 targetPosition;

	SpriteRenderer shapeSriteRenderer;
    SpriteRenderer backgroundSpriteRenderer;
	ScoreCounter scoreCounter;

	GameArea gameArea;
	SoundsManager soundsManager;

	[SerializeField]
	GameObject wrong;

	[SerializeField]
	GameObject right;

    [SerializeField]
    GameObject dust;

	void Awake() {
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();
        shapeSriteRenderer = GetComponentInChildren<ShapeInner> ().GetComponent<SpriteRenderer> ();

		scoreCounter = FindObjectOfType<ScoreCounter> ();

		gameArea = FindObjectOfType<GameArea> ();
		soundsManager = FindObjectOfType<SoundsManager> ();

		count++;
	}

	// Use this for initialization
	void Start () {
		startPosition = gameObject.transform.position;
		targetPosition = new Vector3 (0, 0, -1);

        StartCoroutine(Dust());
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			currentTime += Time.deltaTime;
			transform.position = Vector3.Lerp (startPosition, targetPosition, currentTime / time);
		}
	}

    public void FastForward(float speed) {
        currentTime += speed;
    }

	void OnDestroy() {
		count--;
	}

	void OnTriggerEnter2D(Collider2D col) {
        if (!isActive) {
            return;
        }

        StaticShape shape = col.gameObject.GetComponent<StaticShape> ();

        if (shape) {
            CollisionWithShape(shape);
            return;
        }

        if (!col.gameObject.GetComponent<IgnoredShape>()) {
            // ignore collisions with this shape objects
            CollisionWithWrongObject();
        }
	}

    void CollisionWithShape(StaticShape shape) {
        MatchResult matchResult = shape.Match(this);

        switch (matchResult) {
            case MatchResult.Ok:
                CollisionWithRighObject();
                break;
            case MatchResult.Wrong:
                CollisionWithWrongObject();
                break;
        }
    }

    void CollisionWithRighObject() {
        // increment score
        scoreCounter.Score += GameManager.Level;

        // remove with effect
        if (soundsManager) {
            soundsManager.Match();
        }

        ShowCollisionAnimation(right);
    }

    void CollisionWithWrongObject() {
        // collision with another object, decrement lives
        gameArea.LostLive();

        // destroy with effect
        if (soundsManager) {
            soundsManager.Explode();
        }

        ShowCollisionAnimation(wrong);
    }

    void ShowCollisionAnimation(GameObject prefab) {
        GameObject animationObject = Instantiate(
            prefab, transform.position, Quaternion.identity
        );

        SpriteRenderer spriteRenderer = animationObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = backgroundSpriteRenderer.color;

        isActive = false;
        StartCoroutine(Hide());
    }

    IEnumerator Hide() {
        transform.localScale = new Vector2(0, 0);

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

	public void MoveIn(float time) {
		this.time = time;
	}

	public Sprite Shape {
		get {
            return shapeSriteRenderer.sprite;
		}

		set {
            shapeSriteRenderer.sprite = value;
            shapeSet = true;
		}
	}

	public Color Color {
		get {
            return shapeSriteRenderer.color;
		}

		set {
            shapeSriteRenderer.color = value;
            colorSet = true;

            // set border as well if there isn't shape defined
            if (!IsShapeSet()) {
                backgroundSpriteRenderer.color = value;
            }
        }
	}

	public static int Count {
		get {
			return count;
		}
	}

	public void Pause() {
        isPaused = true;
	}

	public void Resume() {
        isPaused = false;
	}

    public bool IsColorSet() {
        return colorSet;
    }

    public bool IsShapeSet() {
        return shapeSet;
    }

    IEnumerator Dust() {
        while (isActive) {
            GameObject dustElement = Instantiate(
                dust, transform.position, Quaternion.identity
            );

            SpriteRenderer spriteRenderer = dustElement.GetComponent<SpriteRenderer>();
            spriteRenderer.color = backgroundSpriteRenderer.color;

            yield return new WaitForSeconds(0.5f);
        }
    }
}

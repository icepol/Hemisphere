using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShape : MonoBehaviour {

	static int count = 0;

	float time;
	float currentTime = 0;
	bool isPaused = false;
    bool isActive = true;

	Vector3 startPosition;
	Vector3 targetPosition;

	SpriteRenderer spriteRenderer;
	ScoreCounter scoreCounter;

	GameArea gameArea;
	SoundsManager soundsManager;

	[SerializeField]
	GameObject wrong;

	[SerializeField]
	GameObject right;

	void Awake() {
		spriteRenderer = gameObject.GetComponentInChildren<ShapeInner> ().GetComponent<SpriteRenderer> ();

		scoreCounter = FindObjectOfType<ScoreCounter> ();

		gameArea = FindObjectOfType<GameArea> ();
		soundsManager = FindObjectOfType<SoundsManager> ();

		count++;
	}

	// Use this for initialization
	void Start () {
		startPosition = gameObject.transform.position;
		targetPosition = new Vector3 (0, 0, -1);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			currentTime += Time.deltaTime;
			transform.position = Vector3.Lerp (startPosition, targetPosition, currentTime / time);
		}
	}

	void OnDestroy() {
		count--;
	}

	void OnTriggerEnter2D(Collider2D collider) {
        if (!isActive) {
            return;
        }

		Shape shape = collider.gameObject.GetComponent<Shape> ();
		if (shape && shape.Match (this)) {
			// increment score
			scoreCounter.Score += GameManager.Level;

			// remove with effect
			if (soundsManager) {
				soundsManager.Match ();
			}

			// create animation
			Instantiate(right, transform.position, Quaternion.identity);

            isActive = false;
            StartCoroutine(Hide());
		} else {
			// collision with another object, decrement lives
			gameArea.LostLive ();
			
			// destroy with effect
			if (soundsManager) {
				soundsManager.Explode ();
			}

			Instantiate(wrong, transform.position, Quaternion.identity);

            isActive = false;
            StartCoroutine(Hide());
		}
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
			return spriteRenderer.sprite;
		}

		set {
			spriteRenderer.sprite = value;
		}
	}

	public Color Color {
		get {
			return spriteRenderer.color;
		}

		set {
			spriteRenderer.color = value;
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
}

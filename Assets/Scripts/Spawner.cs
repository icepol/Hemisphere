using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	GameArea gameArea;

	[SerializeField]
	float moveSpeed = 2f;

	float currentSpeed;

	[SerializeField]
	float moveSpeedDecrement = 0.05f;

	[SerializeField]
	float levelTehreshold = 10f;

	[SerializeField]
	GameObject movingShape;

	float height;
	float width;

	bool spawning = false;
	SoundsManager soundsManager;

	void Awake() {
		gameArea = FindObjectOfType<GameArea> ();
		soundsManager = GameObject.FindObjectOfType<SoundsManager> ();
	}

	// Use this for initialization
	void Start () {
		height = Camera.main.orthographicSize;
		width = Camera.main.aspect * height;

		currentSpeed = moveSpeed;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!spawning) {
			return;
		}

		Spawning ();
	}

	void Spawning() {
		if (MovingShape.Count < 1) {
			SpawnShape ();
		}

		// speed up moving
		currentSpeed -= moveSpeedDecrement * Time.deltaTime;

		if (currentSpeed <= levelTehreshold) {
			// set speed back to normal
			currentSpeed = moveSpeed;

			// increment level
			GameManager.IncrementLevel();
		}
	}

	void SpawnShape() {
		GameObject spawned = Instantiate (
			movingShape,
			new Vector3(width * (Random.Range(-1f, 1f) > 0 ? 1 : -1), Random.Range(-height, height), -1),
			Quaternion.identity
		) as GameObject;
		spawned.transform.SetParent(gameObject.transform);

		MovingShape shape = spawned.GetComponent<MovingShape> ();
		shape.MoveIn (currentSpeed);

		// set random color
		shape.Color = gameArea.GetRandomColor();

		// set random shape
		shape.Shape = gameArea.GetRandomShape();

		if (soundsManager) {
			soundsManager.Swap ();
		}
	}

	public void StartSpawning() {
		spawning = true;
	}

	public void StopSpawning() {
		spawning = false;

		// remove all childs
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}
	}

	public void Pause() {
		spawning = false;

		// stop moving
		foreach (Transform child in transform) {
			child.gameObject.GetComponent<MovingShape>().Pause();
		}
	}

	public void Resume() {
		spawning = true;

		// stop moving
		foreach (Transform child in transform) {
			child.gameObject.GetComponent<MovingShape>().Resume();
		}
	}
}

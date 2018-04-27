using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	GameArea gameArea;

	[SerializeField]
	float moveSpeed = 2f;

	float currentSpeed;

	[SerializeField]
	float moveSpeedDecrement = 0.15f;

	[SerializeField]
	float levelTehreshold = 8f;

	[SerializeField]
	GameObject movingShape;

	float height;
	float width;

	bool spawning = false;
    int spawnLimit;

	SoundsManager soundsManager;

	void Awake() {
		gameArea = FindObjectOfType<GameArea> ();
		soundsManager = FindObjectOfType<SoundsManager> ();
	}

	// Use this for initialization
	void Start () {
		height = Camera.main.orthographicSize;
		width = Camera.main.aspect * height;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (spawning) {
            Spawning();
		}
	}

	void Spawning() {
		if (MovingShape.Count < 1) {
            // should we increment level?
            if (spawnLimit <= 0) {
                StopSpawning();

                // increment level
                GameManager.Level += 1;
            }
            else {
                // we should spawn next item
                SpawnShape();
            }
		}

        IncreaseSpeed();
	}

    void IncreaseSpeed() {
        // speed up moving
        currentSpeed -= moveSpeedDecrement * Time.deltaTime;

        if (currentSpeed <= levelTehreshold) {
            // set speed back to normal
            currentSpeed = levelTehreshold;
        }
    }

	void SpawnShape() {
        spawnLimit -= 1;

        if (soundsManager) {
            soundsManager.Spawn();
        }

        if (GameManager.Level > 2 && Random.Range(0, 10) > 8) {
            SpawnCombo();
        }
        else {
            SpawnSimpleShape();
        }
	}

    void SpawnSimpleShape() {
        GameObject spawned = Instantiate(
            movingShape,
            new Vector3(
                width * (Random.Range(-1f, 1f) > 0 ? 1 : -1),
                Random.Range(-height, height),
                -1
            ), Quaternion.identity
        ) as GameObject;
        spawned.transform.SetParent(gameObject.transform);

        MovingShape shape = spawned.GetComponent<MovingShape>();
        shape.MoveIn(currentSpeed);

        // set random shape
        shape.Shape = gameArea.GetRandomShape();

        // set random color
        shape.Color = gameArea.GetRandomColor();
    }

    void SpawnCombo() {
        // position
        float position = width * (Random.Range(-1f, 1f) > 0 ? 1 : -1);
              
        // 1. color
        GameObject spawned = Instantiate(
            movingShape,
            new Vector3(
                position,
                Random.Range(-height, height),
                -1
            ), Quaternion.identity
        ) as GameObject;
        spawned.transform.SetParent(gameObject.transform);

        MovingShape shape = spawned.GetComponent<MovingShape>();
        shape.MoveIn(currentSpeed);

        // set random color
        shape.Color = gameArea.GetRandomColor();

         // 2. shape
        spawned = Instantiate(
            movingShape,
            new Vector3(
                position * -1,
                Random.Range(-height, height),
                -1
            ), Quaternion.identity
        ) as GameObject;
        spawned.transform.SetParent(gameObject.transform);

        shape = spawned.GetComponent<MovingShape>();
        shape.MoveIn(currentSpeed);

        // set random shape
        shape.Shape = gameArea.GetRandomShape();
    }

	public void StartSpawning() {
        currentSpeed = moveSpeed;

        spawnLimit = 10 + (GameManager.Level - 1) * 4;
            
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

    public void Hide() {
        StopSpawning();
    }

	public void Resume() {
		spawning = true;

		// stop moving
		foreach (Transform child in transform) {
			child.gameObject.GetComponent<MovingShape>().Resume();
		}
	}
}

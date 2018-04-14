using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlls : MonoBehaviour {
	
	[SerializeField]
	float ringRotationSpeedMin = 50f;

	[SerializeField]
	float ringRotationSpeedMax = 100f;

	[SerializeField]
	float ringRotationSpeedIncerase = 1f;

	float shapesRingCurrentSpeed;
	float colorsRingCurrentSpeed;

	GameObject shapesRing;
	GameObject colorsRing;

	GameArea gameArea;

	bool leftUpPressed = false;
	bool leftDownPressed = false;

	bool rightUpPressed = false;
	bool rightDownPressed = false;

	bool fastForward = false;
	bool isPaused = false;

	// Use this for initialization
	void Start () {
		gameArea = GetComponent<GameArea> ();

		shapesRingCurrentSpeed = ringRotationSpeedMin;
		colorsRingCurrentSpeed = ringRotationSpeedMin;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPaused) {
			HandleControls ();
		}

	}

	void HandleControls() {
		if (leftUpPressed || Input.GetKey(KeyCode.A)) {
			LeftUp ();
		}

		if (leftDownPressed || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Y)) {
			LeftDown ();
		}

		if (rightUpPressed || Input.GetKey(KeyCode.K)) {
			RightUp ();
		}

		if (rightDownPressed || Input.GetKey(KeyCode.M)) {
			RightDown ();
		}

		if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.Z) || Input.GetKeyUp (KeyCode.Y)) {
			shapesRingCurrentSpeed = ringRotationSpeedMin;
		}

		if (Input.GetKeyUp (KeyCode.K) || Input.GetKeyUp (KeyCode.M)) {
			colorsRingCurrentSpeed = ringRotationSpeedMin;
		}

		if (fastForward) {
			FastForward();
		}
	}

	// button pressed handler
	public void ButtonPressed(string buttonId) {
		switch (buttonId) {
		case "LeftUp":
			leftUpPressed = true;
			break;
		case "LeftDown":
			leftDownPressed = true;
			break;
		case "RightUp":
			rightUpPressed = true;
			break;
		case "RightDown":
			rightDownPressed = true;
			break;
		case "FastForward":
			fastForward = true;
			break;
		}
	}

	// button released handler
	public void ButtonReleased(string buttonId) {
		switch (buttonId) {
		case "LeftUp":
			leftUpPressed = false;
			shapesRingCurrentSpeed = ringRotationSpeedMin;
			break;
		case "LeftDown":
			leftDownPressed = false;
			shapesRingCurrentSpeed = ringRotationSpeedMin;
			break;
		case "RightUp":
			rightUpPressed = false;
			colorsRingCurrentSpeed = ringRotationSpeedMin;
			break;
		case "RightDown":
			rightDownPressed = false;
			colorsRingCurrentSpeed = ringRotationSpeedMin;
			break;
		case "FastForward":
			fastForward = false;
			break;
		}
	}

	void IncreaseSahpesRingSpeed() {
		shapesRingCurrentSpeed += shapesRingCurrentSpeed * Time.deltaTime * ringRotationSpeedIncerase;
		shapesRingCurrentSpeed = Mathf.Clamp(shapesRingCurrentSpeed, ringRotationSpeedMin, ringRotationSpeedMax);
	}

	void IncreaseColorsRingSpeed() {
		colorsRingCurrentSpeed += colorsRingCurrentSpeed * Time.deltaTime * ringRotationSpeedIncerase;
		colorsRingCurrentSpeed = Mathf.Clamp(colorsRingCurrentSpeed, ringRotationSpeedMin, ringRotationSpeedMax);
	}

	public void LeftUp() {
		IncreaseSahpesRingSpeed ();
		gameArea.ShapesRing.transform.Rotate (Vector3.forward * Time.deltaTime * shapesRingCurrentSpeed);
	}

	public void LeftDown() {
		IncreaseSahpesRingSpeed ();
		gameArea.ShapesRing.transform.Rotate (Vector3.back * Time.deltaTime * shapesRingCurrentSpeed);
	}

	public void RightUp() {
		IncreaseColorsRingSpeed ();
		gameArea.ColorsRing.transform.Rotate (Vector3.forward * Time.deltaTime * colorsRingCurrentSpeed);
	}

	public void RightDown() {
		IncreaseColorsRingSpeed ();
		gameArea.ColorsRing.transform.Rotate (Vector3.back * Time.deltaTime * colorsRingCurrentSpeed);
	}

	public void Pause() {
		isPaused = true;
	}

	public void Resume() {
		isPaused = false;
	}

	public void FastForward() {
		
	}
}

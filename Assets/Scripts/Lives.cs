using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour {

	[SerializeField]
	Text[] liveElements;

	int lives = 3;
	Animator animator;

	void Awake() {
		animator = GetComponent<Animator> ();
	}

	void Start() {
		UpdateDisplay();
	}

	public int CurrentLives {
		get {
			return lives;
		}

		set {
			if (lives != value) {
				animator.SetTrigger ("Flash");
			}

			lives = value;

			UpdateDisplay ();
		}
	}

	void UpdateDisplay() {
		for (int i = 0; i < liveElements.Length; i++) {
			Color color = liveElements [i].color;
			if (lives > i) {
				// active element
				color.a = 1f;
				liveElements [i].color = color;
			} else {
				// inactive element
				color.a = 0.1f;
				liveElements [i].color = color;
			}
		}
	}
}

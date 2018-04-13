using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	[SerializeField]
	Text scoreText;

	int score;

	Animator animator;

	void Awake () {
		animator = scoreText.GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	public int Score {
		get {
			return score;
		}

		set {
			if (value > score) {
				// flash
				animator.SetTrigger("Flash");
			}

			score = value;

			if (scoreText) {
				// update display
				scoreText.text = score.ToString ();
			}
		}
	}
}

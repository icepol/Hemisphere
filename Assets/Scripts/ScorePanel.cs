﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {

	[SerializeField]
	Text scoreText;

	[SerializeField]
	Text topText;

	SoundsManager soundsManager;

	void Awake() {
		soundsManager = GameObject.FindObjectOfType<SoundsManager> ();
	}

	// Use this for initialization
	void Start () {
		scoreText.text = Settings.LastScore.ToString ();
		topText.text = Settings.TopScore.ToString ();

		if (Settings.IsNewTop) {
			if (soundsManager) {
				soundsManager.Top ();
			}
		}
	}
}

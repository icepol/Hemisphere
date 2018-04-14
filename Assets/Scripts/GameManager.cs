using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

	public delegate void OnLevelIncremented ();

	public static event OnLevelIncremented onLevelIncremented;

	static int level = 1;

	GameManager instance;
	SoundsManager soundsManager;

	void Awake() {
		soundsManager = GameObject.FindObjectOfType<SoundsManager> ();

		DontDestroyOnLoad (gameObject);
	}

	public static int Level {
		get {
			return level;
		}

		set {
			level = value;
		}
	}

	public static void IncrementLevel() {
		level++;

		onLevelIncremented ();
	}

	public void LoadScene(string scene) {
		AnalyticsEvent.ScreenVisit(scene);
		SceneManager.LoadScene (scene);
	}

	public void ButtonClick(string scene) {
		if (soundsManager) {
			soundsManager.ButtonClick ();
		}

		AnalyticsEvent.ScreenVisit(scene);
		SceneManager.LoadScene (scene);
	}
}
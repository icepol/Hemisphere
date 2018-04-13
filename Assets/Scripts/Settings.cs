using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

	const string SOUNDS_KEY = "sounds";
	const string TOP_SCORE_KEY = "top_score";
	const string LAST_SCORE_KEY = "last_score";
	const string IS_NEW_TOP_KEY = "is_new_top";

	public static bool Sounds {
		get {
			return PlayerPrefs.GetInt (SOUNDS_KEY, 1) > 0;
		}

		set {
			PlayerPrefs.SetInt (SOUNDS_KEY, value ? 1 : 0);
		}
	}

	public static int TopScore {
		get {
			return PlayerPrefs.GetInt (TOP_SCORE_KEY, 0);
		}

		set {
			PlayerPrefs.SetInt (TOP_SCORE_KEY, value);
		}
	}

	public static int LastScore {
		get {
			return PlayerPrefs.GetInt (LAST_SCORE_KEY, 0);
		}

		set {
			PlayerPrefs.SetInt (LAST_SCORE_KEY, value);

			if (value > TopScore) {
				TopScore = value;
				IsNewTop = true;
			} else {
				IsNewTop = false;
			}
		}
	}

	public static bool IsNewTop {
		get {
			return PlayerPrefs.GetInt (IS_NEW_TOP_KEY, 1) > 0;
		}

		set {
			PlayerPrefs.SetInt (IS_NEW_TOP_KEY, value ? 1 : 0);
		}
	}
}

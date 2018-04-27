using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

	const string SOUNDS_KEY = "sounds";
	const string TOP_SCORE_KEY = "top_score";
	const string LAST_SCORE_KEY = "last_score";
	const string IS_NEW_TOP_KEY = "is_new_top";
    const string IS_FIRST_RUN = "is_first_run";
    const string SHOW_CONTINUE_BUTTIN = "show_continue_button";

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

    public static bool IsFirstRun {
        get {
            return PlayerPrefs.GetInt(IS_FIRST_RUN, 1) > 0;
        }

        set {
            PlayerPrefs.SetInt(IS_FIRST_RUN, value ? 1 : 0);
        }
    }

    public static bool ShowContinueButton {
        get {
            return PlayerPrefs.GetInt(SHOW_CONTINUE_BUTTIN, 0) > 0;
        }

        set {
            PlayerPrefs.SetInt(SHOW_CONTINUE_BUTTIN, value ? 1 : 0);
        }
    }
}

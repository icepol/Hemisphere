using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScene : MonoBehaviour {

    [SerializeField]
    GameObject backButton;

    [SerializeField]
    GameObject continueButton;

	// Use this for initialization
	void Awake () {
        if (Settings.ShowContinueButton) {
            // show continue button if this is first run
            backButton.SetActive(false);
            continueButton.SetActive(true);

            Settings.ShowContinueButton = false;
        }
	}
}

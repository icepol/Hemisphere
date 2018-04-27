using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {

    GameManager gameManager;

    void Awake() {
        gameManager = FindObjectOfType<GameManager>();

        if (Settings.IsFirstRun) {
            // we will show info screen if this is first
            Settings.IsFirstRun = false;
            Settings.ShowContinueButton = true;
            gameManager.LoadScene("Info");
        }
    }

}

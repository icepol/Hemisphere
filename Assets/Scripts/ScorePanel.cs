﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ScorePanel : MonoBehaviour {

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text topText;

    GameManager gameManager;
    SoundsManager soundsManager;
    Animator animator;

    void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        soundsManager = FindObjectOfType<SoundsManager>();
        animator = FindObjectOfType<Animator>();
    }

    // Use this for initialization
    void Start() {
        scoreText.text = Settings.LastScore.ToString();
        topText.text = Settings.TopScore.ToString();

        // send score
        gameManager.ReportScore(Settings.LastScore);

        StartCoroutine(NewTop());
    }

    IEnumerator NewTop() {
        yield return new WaitForSeconds(1f);

        if (Settings.IsNewTop) {
            // show label
            animator.SetTrigger("NewTopScore");

            yield return new WaitForSeconds(0.8f);

            // play sound
            if (soundsManager) {
                soundsManager.Top();
            }
        }

        if (Advertisement.IsReady()) {
            yield return new WaitForSeconds(1f);

            Advertisement.Show();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    GameManager gameManager;
    SoundsManager soundsManager;
    Animator animator;

	private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        soundsManager = FindObjectOfType<SoundsManager>();
        animator = FindObjectOfType<Animator>();
	}

	void Start () {
        // send score
        gameManager.ReportScore(Settings.LastScore);

        StartCoroutine(NewTop());
	}
	
	// Update is called once per frame
	void Destroy () {
        Ads.DestroyInterstitial();
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

        if (Ads.IsInterstitialLoaded()) {
            yield return new WaitForSeconds(1f);

            Ads.ShowIntertitial();
        }
    }
}

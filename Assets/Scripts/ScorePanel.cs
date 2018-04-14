using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ScorePanel : MonoBehaviour {

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text topText;

    SoundsManager soundsManager;
    Animator animator;

    void Awake() {
        soundsManager = FindObjectOfType<SoundsManager>();
        animator = FindObjectOfType<Animator>();
    }

    // Use this for initialization
    void Start() {
        scoreText.text = Settings.LastScore.ToString();
        topText.text = Settings.TopScore.ToString();

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

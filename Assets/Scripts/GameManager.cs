using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GameManager : MonoBehaviour {

    public delegate void OnLevelIncremented();

    public static event OnLevelIncremented onLevelIncremented;

    static int level = 1;
    static bool showAuthentication = true;

    GameManager instance;
    SoundsManager soundsManager;

    void Awake() {
        soundsManager = FindObjectOfType<SoundsManager>();

        DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
#endif

        if (showAuthentication) {
            showAuthentication = false;
            Social.localUser.Authenticate(OnUserAuthenticated);
        }
    }

    void OnUserAuthenticated(bool obj) {
    }

    public static int Level {
        get {
            return level;
        }

        set {
            // level incemented by one
            bool isIncremented = value == level + 1;

            level = value;

            if (isIncremented) {
                onLevelIncremented();
            }
        }
    }

    public void LoadScene(string scene) {
        AnalyticsEvent.ScreenVisit(scene);
        SceneManager.LoadScene(scene);
    }

    void PlayClickSound() {
        if (soundsManager) {
            soundsManager.ButtonClick();
        }
    }

    public void ButtonClick(string scene) {
        PlayClickSound();

        AnalyticsEvent.ScreenVisit(scene);
        SceneManager.LoadScene(scene);
    }

    public void ShowLeaderBoard() {
        PlayClickSound();

#if UNITY_IPHONE
        Social.ShowLeaderboardUI();
#elif UNITY_ANDROID
        PlayGamesPlatform.Instance.ShowLeaderboardUI(Constants.LeaderBoardId);
#else
#endif
    }

    public void ReportScore(int score) {
        Social.ReportScore(
            score, Constants.LeaderBoardId, OnReportScore
        );

    }

    void OnReportScore(bool obj) {
    }
}
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
    static bool isAuthenticated = false;

    void Awake() {
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
        isAuthenticated = true;
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

    public void ButtonClick(string scene) {
        SoundsManager.Instance.ButtonClick();

        AnalyticsEvent.ScreenVisit(scene);
        SceneManager.LoadScene(scene);
    }

    public void ShowLeaderBoard() {
        SoundsManager.Instance.ButtonClick();

        if (!isAuthenticated) {
            Social.localUser.Authenticate(OnUserAuthenticated);
        }

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
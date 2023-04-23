using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    public static readonly string lastPlayedLevelKey = "gm_lastPlayedLevel";
    public static readonly string cumulativeLevelsPlayedKey = "gm_cumulativeLevels";

    [Header("Debug")]
    public bool isLevelActive = false;
    public bool isLevelSuccessful = false;

    public event System.Action LevelStartedEvent;
    public event System.Action LevelEndedEvent; // fired regardless of fail or success
    public event System.Action LevelSuccessEvent; // fired only on success
    public event System.Action LevelFailedEvent; // fired only on fail

    protected override void Awake()
    {
        base.Awake();


    }

    public void StartGame() // LevelStarted
    {
        isLevelActive = true;
        LevelStartedEvent?.Invoke();
    }

    public void EndGame(bool success)
    {
        isLevelActive = false;
        isLevelSuccessful = success;

        LevelEndedEvent?.Invoke();

        if (success) LevelSuccessEvent?.Invoke();
        else LevelFailedEvent?.Invoke();
    }

    public void NextStage()
    {

    }

    public void RestartStage()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadScene(int targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }

    public int GetTotalStagePlayed()
    {
        return PlayerPrefs.GetInt(cumulativeLevelsPlayedKey, 1);
    }
}

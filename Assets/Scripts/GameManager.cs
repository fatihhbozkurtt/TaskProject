using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    [Header("Debug")]
    public bool isLevelActive = false;

    public event System.Action LevelStartedEvent;
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

        if (success) LevelSuccessEvent?.Invoke();
        else LevelFailedEvent?.Invoke();
    }

    public void NextStage()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex); // for this task there is no next level, that is why I activate the same level
    }

    public void RestartStage()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadScene(int targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}

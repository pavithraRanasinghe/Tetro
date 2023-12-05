using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private const string HighScoreKey = "HighScore";
    private const string TotalScoreKey = "TotalScore";

    public int HighScore
    {
        get => PlayerPrefs.GetInt(HighScoreKey,0);
        set => PlayerPrefs.SetInt(HighScoreKey, value);
    }
    public int TotalScore
    {
        get => PlayerPrefs.GetInt(TotalScoreKey,0);
        set => PlayerPrefs.SetInt(TotalScoreKey, PlayerPrefs.GetInt(TotalScoreKey)+value);
    }

    public int CurrentScore { get; set; }
    public bool IsInitialized { get; set; }


    private void Init()
    {
        CurrentScore = 0;
        IsInitialized = false;
    }

    private const string MainMenu = "MainMenu";
    private const string Gameplay = "Gameplay";

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void GoToGameplay()
    {
        SceneManager.LoadScene(Gameplay);
    }
}

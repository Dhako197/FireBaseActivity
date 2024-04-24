using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeToReloadScene;

    [SerializeField] private GameObject DayBackground;
    [SerializeField] private GameObject NightBackground;
    [SerializeField] private GameObject ScoreCanva;

    [Space, SerializeField] private UnityEvent onStartGame;
    [SerializeField] private UnityEvent onGameOver, onIncreaseScore;

    public int maxScore= 0;

    public bool isDay;

    public int score
    {
        get;
        private set;
    }

    public bool isGameOver
    {
        get;
        private set;
    }

    // Singleton!
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);

        float randomDay = Random.Range(0, 2);
        switch (randomDay)
        {
            case 0:
                isDay = true;
                DayBackground.SetActive(true);
                break;
            case 1:
                isDay = false;
                NightBackground.SetActive(true);
                break;
        }

    }

    public void StartGame()
    {
        Debug.Log("GameManager :: StartGame()");

        onStartGame?.Invoke();
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        Debug.Log("GameManager :: GameOver()");

        isGameOver = true;

        onGameOver?.Invoke();

        if (SaveBestScore.Instance.BestScore < score)
            SaveBestScore.Instance.BestScore = score;
        UIManager.Instance.UpdateMaxScore();
        
        ScoreCanva.SetActive(true);

        StartCoroutine(ReloadScene());
    }

    public void IncreaseScore()
    {
        Debug.Log("GameManager :: IncreaseScore()");

        score++;

        onIncreaseScore?.Invoke();
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(timeToReloadScene);

        Debug.Log("GameManager :: ReloadScene()");

        SceneManager.LoadScene(0);
    }
}

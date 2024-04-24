using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI finalscore;
    [SerializeField] private TextMeshProUGUI bestScore;

    // Singleton!
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this.gameObject);
    }

    public void UpdateScoreText()
    {
        Debug.Log("UIManager :: UpdateScoreText()");

        score.text = GameManager.Instance.score.ToString();
        
    }

    public void UpdateMaxScore( )
    {
        finalscore.text = GameManager.Instance.score.ToString();
        bestScore.text = SaveBestScore.Instance.BestScore.ToString();
    }
}

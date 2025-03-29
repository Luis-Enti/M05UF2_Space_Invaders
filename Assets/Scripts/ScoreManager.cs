using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;

    private int currentScore;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddEnemyScore(int enemyRow)
    {
        int points = 0;

        switch (enemyRow)
        {
            case 0:
            case 1:
                points = 10;
                break;
            case 2:
            case 3:
                points = 20;
                break;
            case 4:
                points = 30;
                break;
            default:
                points = 10;
                break;
        }
        Debug.Log(points);

        AddScore(points);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
        Debug.Log("Puntos añadidos: " + points + " | Total: " + currentScore);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }
}
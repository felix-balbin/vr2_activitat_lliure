using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI textScore;

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
        GameManager.instancia.CheckScore(score);
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }

    public void UpdateScore()
    {
        textScore.text = "Score: " + score;
    }
}
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score = 0;
    private bool first = true;
    public TextMeshProUGUI textScore;

    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
        
        if (first && score >= 30)
        {
            GameManager.instancia.CheckScore(score);
            ResetScore();
            first = false;
        }
        else if (!first && score >= 30) {
            GameManager.instancia.CheckScore(score);
            ResetScore();
        }
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
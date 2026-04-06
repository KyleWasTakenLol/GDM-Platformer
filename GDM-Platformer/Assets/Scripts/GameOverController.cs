using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TMP_InputField playerNameInput;

    void Start()
    {
        int finalScore = GameManager.Instance.GetScore();
        scoreText.text = "Final Score: " + finalScore;
    }

    public void OnSubmitScore()
    {
        string playerName = playerNameInput.text;

        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Anonymous";
        }

        int finalScore = GameManager.Instance.GetScore();
        float completionTime = Time.timeSinceLevelLoad;

        DatabaseManager.Instance.SaveHighScore(playerName, finalScore, completionTime);

        SceneManager.LoadScene("HighScores");
    }

    public void Retry()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("GameScene");
    }
}
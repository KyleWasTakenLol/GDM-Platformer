using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.SetInt("FinalScore", 0);
        SceneManager.LoadScene("GameScene");
    }
}
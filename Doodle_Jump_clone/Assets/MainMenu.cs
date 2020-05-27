
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    private void Awake()
    {
        scoreText.text = PlayerPrefs.GetFloat("Score", 0).ToString("0");
        highScoreText.text = PlayerPrefs.GetFloat("Highscore", 0).ToString("0");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene("Menu");
    }
}

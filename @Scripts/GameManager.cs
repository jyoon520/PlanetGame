using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button pauseButton;
    public Button reStartButton;
    public Button homeButton;
    public GameObject pauseUI;

    void Start()
    {
        pauseButton.onClick.AddListener(OnpauseButtonClick);
        reStartButton.onClick.AddListener(OnreStartButtonClick);
        homeButton.onClick.AddListener(OnhomeButtonClick);
    }

    void OnpauseButtonClick()
    {
        if (!pauseUI.activeSelf)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else if (pauseUI.activeSelf)
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }

    void OnreStartButtonClick()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    void OnhomeButtonClick()
    {
        SceneManager.LoadScene("Lobby");
    }
}

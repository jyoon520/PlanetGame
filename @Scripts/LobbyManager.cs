using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Button gameStartButton;
    void Start()
    {
        gameStartButton.onClick.AddListener(OngameStartButtonClick);
    }

    void OngameStartButtonClick()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
}

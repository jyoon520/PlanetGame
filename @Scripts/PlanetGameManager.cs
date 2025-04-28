using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetGameManager : MonoBehaviour
{
    public Planet lastPlanet;
    public GameObject planetPrefab;
    public GameObject GameOverUI;
    public GameObject mergeEffect;
    public Transform planetGroup;
    public Text scoreText;
    public Button reStartButton;


    public int maxLevel;
    public int score;
    

    void Start()
    {
        NextPlanet();
        reStartButton.onClick.AddListener(OnreStartButtonClick);
    }

    Planet GetPlanet()
    {
        GameObject instantPlanetObj = Instantiate(planetPrefab, planetGroup);
        Planet instantPlanet = instantPlanetObj.GetComponent<Planet>();
        return instantPlanet;
    }

    void NextPlanet()
    {
        Planet newPlanet = GetPlanet();
        lastPlanet = newPlanet;
        lastPlanet.manager = this;

        int randomLevel = Random.Range(0, maxLevel);
        lastPlanet.SetLevel(randomLevel);

        lastPlanet.gameObject.SetActive(true);

        StartCoroutine(WaitNext());
    }

    IEnumerator WaitNext()
    {
        while (lastPlanet != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);
        NextPlanet();
    }

    public void TouchDown()
    {
        if (lastPlanet == null)
            return;

        lastPlanet.Drag();
    }

    public void TouchUp()
    {
        if (lastPlanet == null)
            return;

        lastPlanet.Drop();
        lastPlanet = null;
    }

    void OnreStartButtonClick()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
}

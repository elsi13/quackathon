using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI DistanceText;
    public GameObject titleScreen;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private float score;
    private float distance;
    public bool isGameActive;
    [SerializeField] GameObject [] assets;
    public float horizontalBound = 3.0f;
    float spawnRateMin = 1;
    float spawnRateMax = 2;
    float weightEnemy = 40;
    float weightPowerup = 10;
    float weightPoint = 10;
    float weightObsticle = 10;
    float period = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnObjects()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));
            int index = (int)RandomRange.Range(
                new FloatRange(0f, 1f, weightEnemy),
                new FloatRange(1f, 2f, weightPowerup),
                new FloatRange(2f, 3f, weightPoint),
                new FloatRange(3f, 4f, weightObsticle)
                );
            Instantiate(assets[index], RandomStartPos(), assets[index].transform.rotation);
            Debug.Log("Spawn enemy");
        }
    }

    IEnumerator DistanceUpdate()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            UpdateDistance(1);
            Time.timeScale = Time.timeScale + period;
        }
    }

    Vector3 RandomStartPos()
    {
        float randomX = Random.Range(-horizontalBound, horizontalBound);
        Debug.Log(randomX);
        return new Vector3(randomX, 1, 30);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateDistance(int distanceToAdd)
    {
        distance += distanceToAdd;
        DistanceText.text = "Distance: " + distance + " km";
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        distance = 0;
        UpdateScore(0);
        UpdateDistance(0);
        StartCoroutine(SpawnObjects());
        StartCoroutine(DistanceUpdate());

        titleScreen.SetActive(false);
    }
}

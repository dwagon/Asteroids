using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] int numAsteroids;
    [SerializeField] int maxNumLives = 3;
    [SerializeField] TMP_Text lifeText;
    [SerializeField] float timeToWaitForPlayerLife = 2f;
    [SerializeField] float timeToSpawnAsteroid = 5f;

    Spawner asteroidSpawner;
    Player player;
    int score = 0;
    int numLives;
    bool isSpawning = false;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
        asteroidSpawner = FindFirstObjectByType<Spawner>();
        player = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        scoreText.text = score.ToString();
        numLives = maxNumLives;
        lifeText.text = numLives + " Lives";

        asteroidSpawner.SpawnAsteroids(numAsteroids);
    }

    void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void KilledAsteroid(int asteroid_score)
    {
        AddScore(asteroid_score);
    }


    void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnAsteroid());
        }
    }

    public void PlayerDeath()
    {
        numLives--;
        lifeText.text = numLives + " Lives";
        if (numLives > 0)
        {
            StartCoroutine(RestartPlayer());
        }
        else
        {
            StartCoroutine(RestartGame());
            numLives = maxNumLives;
            score = 0;
        }
    }

    IEnumerator SpawnAsteroid()
    {
        yield return new WaitForSeconds(timeToSpawnAsteroid);
        asteroidSpawner.SpawnAsteroid();
        isSpawning = false;
    }

    IEnumerator RestartPlayer()
    {
        yield return new WaitForSeconds(timeToWaitForPlayerLife);
        player.PlayerAlive();

    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(timeToWaitForPlayerLife);
        SceneManager.LoadScene(0);
    }

}

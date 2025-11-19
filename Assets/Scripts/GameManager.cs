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
    [SerializeField] float timeToWaitForPlayerLife = 2;

    int currentAsteroids;
    Spawner asteroidSpawner;
    Player player;
    int score = 0;
    int numLives;


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

        currentAsteroids = asteroidSpawner.Spawn(numAsteroids).Count;
    }

    void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void KilledAsteroid(int asteroid_score)
    {
        currentAsteroids--;
        AddScore(asteroid_score);
    }

    public void CreatedAsteroid()
    {
        currentAsteroids++;
    }

    void Update()
    {
        if (currentAsteroids < numAsteroids)
        {
            currentAsteroids += asteroidSpawner.Spawn(numAsteroids - currentAsteroids).Count;
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

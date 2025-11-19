using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] int numAsteroids;
    [SerializeField] int numLives = 3;
    [SerializeField] TMP_Text lifeText;
    [SerializeField] float timeToWaitForPlayerLife = 2;

    int currentAsteroids;
    Spawner asteroidSpawner;
    Player player;
    int score = 0;


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
    }

    IEnumerator RestartPlayer()
    {
        yield return new WaitForSeconds(timeToWaitForPlayerLife);
        player.PlayerAlive();
    }

}

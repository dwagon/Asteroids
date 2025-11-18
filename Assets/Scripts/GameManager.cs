using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] int numAsteroids;
    int currentAsteroids;
    Spawner asteroidSpawner;
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
    }

    void Start()
    {
        scoreText.text = score.ToString();
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

}

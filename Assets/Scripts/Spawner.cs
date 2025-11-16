using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject initial;
    [SerializeField] int numToSpawn;

    void Start()
    {
        Asteroid asteroid;
        for (int i = 0; i < numToSpawn; i++)
        {
            asteroid = Instantiate(initial).GetComponent<Asteroid>();
            asteroid.transform.position = asteroid.GeneratePosition();
        }
    }

    void Update()
    {
    }
}

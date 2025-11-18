using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject initial;

    public List<Asteroid> Spawn(int numToSpawn)
    {
        List<Asteroid> spawned = new List<Asteroid>();
        Asteroid asteroid;
        for (int i = 0; i < numToSpawn; i++)
        {
            asteroid = Instantiate(initial).GetComponent<Asteroid>();
            asteroid.transform.position = asteroid.GeneratePosition();
            spawned.Add(asteroid);
        }
        return spawned;
    }

    void Update()
    {
    }
}

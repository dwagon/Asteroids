using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject initial;

    public void SpawnAsteroids(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnAsteroid();
        }
    }

    public void SpawnAsteroid()
    {
        Asteroid asteroid;

        asteroid = Instantiate(initial).GetComponent<Asteroid>();
        asteroid.transform.position = asteroid.GeneratePosition();
    }
}

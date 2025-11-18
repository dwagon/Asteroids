// using System;
// using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(SpriteRenderer))]
public class Asteroid : MonoBehaviour
{

    [SerializeField] float initialMaxVelocity = 1f;
    [SerializeField] float initialMinRotation = 10f;
    [SerializeField] float initialMaxRotation = 20f;
    [SerializeField] bool breakUp = false;
    [SerializeField] GameObject breakInto;
    [SerializeField] int score = 0;
    [SerializeField] AudioClip explosionSound;

    GameManager gameManager;
    Vector3 velocity;
    float rotation;
    Renderer a_renderer;
    Camera a_camera;

    void Awake()
    {
        a_renderer = GetComponent<Renderer>();
        a_camera = FindAnyObjectByType<Camera>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Start()
    {
        velocity = GenerateVelocity();
        rotation = GenerateRotation();
    }


    float GenerateRotation()
    // Initial Rotation Speed - nothing too slow
    {
        float new_rotation;
        new_rotation = UnityEngine.Random.Range(initialMinRotation, initialMaxRotation);
        float rotation_dir = UnityEngine.Random.Range(0, 1);
        if (rotation_dir == 0)
        {
            new_rotation = -new_rotation;
        }
        return new_rotation;
    }

    Vector3 GenerateVelocity()
    // Initial Velocity
    {
        float vel_x = Random.Range(-initialMaxVelocity, initialMaxVelocity);
        float vel_y = Random.Range(-initialMaxVelocity, initialMaxVelocity);
        return new Vector3(vel_x, vel_y, 0f);
    }

    public Vector3 GeneratePosition()
    {
        int side = Random.Range(0, 3);
        Vector3 random_loc = new(Random.Range(0f, 1f), Random.Range(0f, 1f), a_camera.nearClipPlane);
        switch (side)
        {
            case 0:
                random_loc.y = 1f;
                break;
            case 1:
                random_loc.x = 1f;
                break;
            case 2:
                random_loc.y = 0f;
                break;
            case 3:
                random_loc.x = 0f;
                break;
        }
        return a_camera.ViewportToWorldPoint(random_loc);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, rotation * Time.deltaTime));
        transform.position += velocity * Time.deltaTime;
        if (!a_renderer.isVisible)
        {
            CheckOffScreen();
        }
    }

    public void SplitAsteroid()
    {
        Quaternion quat;

        quat = new Quaternion(0f, 0f, GenerateRotation(), 1);
        Instantiate(breakInto, transform.position, quat);
        gameManager.CreatedAsteroid();

        quat = new Quaternion(0f, 0f, GenerateRotation(), 1);
        Instantiate(breakInto, transform.position, quat);
        gameManager.CreatedAsteroid();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullets"))
        {
            if (breakUp)
            {
                SplitAsteroid();
            }
            gameManager.KilledAsteroid(score);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            Destroy(gameObject);

            Destroy(other.gameObject);  // Delete the bullet
        }
    }

    void CheckOffScreen()
    // Make asteroids wrap
    {
        Vector2 screen_position = a_camera.WorldToScreenPoint(transform.position);
        float camera_z = a_camera.nearClipPlane;

        if (screen_position.y < 0)    // Bottom of screen
        {
            var top = a_camera.ScreenToWorldPoint(new Vector3(0f, Screen.height, camera_z));
            transform.position = new Vector3(transform.position.x, top.y, 0f);
        }
        else if (screen_position.y > Screen.height)    // Top of screen
        {
            var bottom = a_camera.ScreenToWorldPoint(new Vector3(0f, 0f, camera_z));
            transform.position = new Vector3(transform.position.x, bottom.y, 0f);

        }
        else if (screen_position.x < 0) // Left of screen
        {
            var right = a_camera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, camera_z));
            transform.position = new Vector3(right.x, transform.position.y, 0f);
        }
        else if (screen_position.x > Screen.width)    // Right of screen
        {
            var left = a_camera.ScreenToWorldPoint(new Vector3(0f, 0f, camera_z));
            transform.position = new Vector3(left.x, transform.position.y, 0f);
        }
    }
}

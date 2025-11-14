using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Asteroid : MonoBehaviour
{

    [SerializeField] float maxVelocity = 1f;
    [SerializeField] float minRotation = 10f;
    [SerializeField] float maxRotation = 20f;

    Vector3 velocity;
    float rotation;
    Renderer a_renderer;
    float startTime;

    void Start()
    {
        velocity = GenerateVelocity();
        rotation = GenerateRotation();

        a_renderer = GetComponent<Renderer>();
        startTime = Time.time;
    }

    float GenerateRotation()
    // Initial Rotation Speed - nothing too slow
    {
        float new_rotation;
        new_rotation = Random.Range(minRotation, maxRotation);
        float rotation_dir = Random.Range(0, 1);
        if (rotation_dir == 0)
        {
            new_rotation = -new_rotation;
        }
        return new_rotation;
    }

    Vector3 GenerateVelocity()
    // Initial Velocity
    {
        float vel_x = Random.Range(-maxVelocity, maxVelocity);
        float vel_y = Random.Range(-maxVelocity, maxVelocity);
        return new Vector3(vel_x, vel_y, 0f);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, rotation * Time.deltaTime));
        transform.position += velocity * Time.deltaTime;
        if (!a_renderer.isVisible && Time.time > startTime + 1) // First update it isn't visible for some reason
        {
            Destroy(gameObject);
        }
    }
}

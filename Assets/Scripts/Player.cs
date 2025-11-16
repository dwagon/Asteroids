using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ParticleSystem))]
public class Player : MonoBehaviour
{

    [SerializeField] GameObject firingPoint;
    [SerializeField] GameObject bulletObject;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] float fireCoolDown = 0.1f;
    float rotation = 0f;
    InputAction rotateAction;
    InputAction fireAction;
    float lastFired;
    ParticleSystem explosion;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        lastFired = Time.time;
        rotateAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Attack");
        explosion = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        DoMovement();
        DoFiring();
    }

    void DoMovement()
    {
        rotation = rotateAction.ReadValue<Vector2>().x * -rotateSpeed * Time.deltaTime;
        transform.RotateAround(new Vector3(0f, 0f, 0f), Vector3.forward, rotation);
    }

    void DoFiring()
    {
        if (fireAction.IsPressed() && Time.time > lastFired + fireCoolDown)
        {
            GameObject new_bullet;
            new_bullet = Instantiate(bulletObject, firingPoint.transform.position, transform.rotation);
            lastFired = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroids"))
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        spriteRenderer.enabled = false;
        explosion.Play();
    }
}

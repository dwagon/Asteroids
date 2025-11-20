using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ParticleSystem))]
public class Player : MonoBehaviour
{

    [SerializeField] GameObject firingPoint;
    [SerializeField] GameObject bulletObject;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] float fireCoolDown = 0.1f;
    [SerializeField] AudioClip shoot_sound;
    [SerializeField] AudioClip playerDeath_sound;

    float rotation = 0f;
    bool isAlive = true;
    InputAction rotateAction;
    InputAction fireAction;
    float lastFired;
    ParticleSystem explosion;
    SpriteRenderer my_spriteRenderer;
    GameManager my_gameManager;

    void Start()
    {
        lastFired = Time.time;
        rotateAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Attack");
        explosion = GetComponent<ParticleSystem>();
        my_gameManager = FindFirstObjectByType<GameManager>();
        my_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        isAlive = true;
    }

    void Update()
    {
        if (isAlive)
        {
            DoMovement();
            DoFiring();
        }
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
            AudioSource.PlayClipAtPoint(shoot_sound, transform.position, 0.5f);
            Instantiate(bulletObject, firingPoint.transform.position, transform.rotation);
            lastFired = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isAlive && other.CompareTag("Asteroids"))
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        isAlive = false;
        my_spriteRenderer.enabled = false;
        explosion.Play();
        AudioSource.PlayClipAtPoint(playerDeath_sound, transform.position);
        my_gameManager.PlayerDeath();
    }

    public void PlayerAlive()
    {
        isAlive = true;
        my_spriteRenderer.enabled = true;

    }
}

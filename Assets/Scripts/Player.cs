using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

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

    void Start()
    {
        lastFired = Time.time;
        rotateAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        DoMovement();
        DoFiring();
    }

    void DoMovement()
    {
        rotation = rotateAction.ReadValue<Vector2>().x * rotateSpeed * Time.deltaTime;
        transform.RotateAround(new Vector3(-0.6f, 0.5f, 0f), Vector3.forward, rotation);
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
}

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float rotation = 0f;
    InputAction rotateAction;

    void Start()
    {
        rotateAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        DoMovement();
    }

    void DoMovement()
    {
        rotation += rotateAction.ReadValue<Vector2>().x;
        transform.eulerAngles = new Vector3(0f, 0f, rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class MovementController : MonoBehaviour
{
    private enum MoveDirection
    {
        up, down, left, right
    }

    public bool freez;
    public float GridSize;
    public static MovementController instance;
    public InputAction moveInput;
    public float speedMutlplier = 1f;

    [Range(0, 1f)]
    public float moveSpeed = 0.23f;

    public Vector2 inputDirection;
    public bool moving;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        moveInput.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        if (moveInput.ReadValue<Vector2>().magnitude > 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if (moveInput.ReadValue<Vector2>() != inputDirection)
        {
            if (moveInput.ReadValue<Vector2>().x != inputDirection.x)
            {
                inputDirection = new Vector2(moveInput.ReadValue<Vector2>().x, 0);
            }
            if (moveInput.ReadValue<Vector2>().y != inputDirection.y)
            {
                inputDirection = new Vector2(0, moveInput.ReadValue<Vector2>().y);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!freez)
        {
            this.transform.Translate(new Vector3(Mathf.Round((inputDirection.x / GridSize) * 10.0f) * 0.01f,
      Mathf.Round((inputDirection.y / GridSize) * 10.0f) * 0.01f, 0) * moveSpeed * speedMutlplier);
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x / GridSize * 100.0f) * 0.01f,
             Mathf.Round(transform.position.y / GridSize * 100.0f) * 0.01f,
             Mathf.Round(transform.position.z / GridSize * 100.0f) * 0.01f);
    }
}
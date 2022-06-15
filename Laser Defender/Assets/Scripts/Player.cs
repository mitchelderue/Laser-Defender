using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 5f;
    Vector2 rawInput;

    [Header("PlayerField")]
    Vector2 minBounds;
    Vector2 MaxBounds;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingLTop;
    [SerializeField] float paddingBottom;

    void Start()
    {
        initBounds();
    }

    void Update()
    {
        Move();
    }

    void initBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        MaxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();

        // Make sure player doesnt move offscreen
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, MaxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, MaxBounds.y - paddingLTop);

        // Apply movement
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log(rawInput);
    }
}

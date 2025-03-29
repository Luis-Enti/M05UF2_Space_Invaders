using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float padding = 0.5f;

    private float minX;
    private float maxX;

    void Start()
    {
        CalculateBounds();
    }

    void CalculateBounds()
    {
        Camera mainCamera = Camera.main;
        Vector3 spriteSize = GetComponent<SpriteRenderer>().bounds.size;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteSize.x / 2 + padding;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spriteSize.x / 2 - padding;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }
}

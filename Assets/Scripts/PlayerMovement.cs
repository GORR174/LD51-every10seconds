using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Rigidbody2D playerRigidBody;
    
    private bool canMove = true;
    private Vector2 movement;
    
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (!canMove)
            movement = Vector2.zero;
    }

    private void FixedUpdate()
    {
        playerRigidBody.MovePosition(playerRigidBody.position + movement * (speed * Time.fixedDeltaTime));
    }
}
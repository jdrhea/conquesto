using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    [SerializeField] float vertical;
    [SerializeField] float horizontal;
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

    }
}

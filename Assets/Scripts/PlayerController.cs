using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRB;

    private float speed = 7.0f;
    private float JumpForce = 7.0f;
    private float Bound = 9.5f; // All invisible walls on the same positions becouse ground is square

    public bool IsOnGround;

    // Used for stop AddForce method for faster moving on another way
    public bool OnLeftBound;
    public bool OnRightBound;
    public bool OnForwardBound;
    public bool OnBackBound;
    

    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        PlayerMovement();
        InvisibleBounds();
    }

    // Use tags for avoid double jump
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
        }
    }

    // All player moves, jump too
    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W) && !OnForwardBound)
        {
            PlayerRB.AddForce(Vector3.forward * speed);
            OnBackBound = false;
        }

        if (Input.GetKey(KeyCode.S) && !OnBackBound)
        {
            PlayerRB.AddForce(Vector3.back * speed);
            OnForwardBound = false;
        }

        if (Input.GetKey(KeyCode.D) && !OnRightBound)
        {
            PlayerRB.AddForce(Vector3.right * speed);
            OnLeftBound = false;
        }

        if (Input.GetKey(KeyCode.A) && !OnLeftBound)
        {
            PlayerRB.AddForce(Vector3.left * speed);
            OnRightBound = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround)
        {
            PlayerRB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            IsOnGround = false;
        }
    }

    // Invisible walls and using OnBound booleans
    private void InvisibleBounds()
    {
        if (transform.position.z > Bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Bound);
            OnForwardBound = true;
        }

        if (transform.position.z < -Bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -Bound);
            OnBackBound = true;
        }

        if (transform.position.x > Bound)
        {
            transform.position = new Vector3(Bound, transform.position.y, transform.position.z);
            OnRightBound = true;
        }

        if (transform.position.x < -Bound)
        {
            transform.position = new Vector3(-Bound, transform.position.y, transform.position.z);
            OnLeftBound = true;
        }
    }
}

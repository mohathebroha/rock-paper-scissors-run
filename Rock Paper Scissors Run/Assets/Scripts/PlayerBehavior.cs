using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehavior : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    public float speed;
    private float hInput = 0.0f;
    public float jumpForce;
    public float gravity = -10;
    // Start is called before the first frame update
    void Start()
    {
        direction.z = speed;
        
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!characterController.isGrounded)
            direction.y += gravity * Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        hInput = Input.GetAxis("Horizontal") * speed;
    }
    private void FixedUpdate()
    {
        characterController.Move(direction * Time.fixedDeltaTime + this.transform.right * hInput * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}

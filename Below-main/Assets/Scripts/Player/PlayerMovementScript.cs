using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    CharacterController CC;
    [SerializeField]
    float speed = 10f;
    [SerializeField]
    float jumpHeight = 5f;
    [SerializeField]
    float jumpSlow = 0.5f;
    [SerializeField]
    float gravity = -9.81f;
    [SerializeField]
    GameObject groundCheck;
    [SerializeField]
    float groundDistance = 0.4f;
    [SerializeField]
    LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    

    // Update is called once per frame
    void Update()
    {
        //checks if grounded
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        Vector3 move = new Vector3(0f,0f,0f);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        move = transform.right * x + transform.forward * z;
        
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        CC.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        CC.Move(velocity * Time.deltaTime);

        

        
    }
}

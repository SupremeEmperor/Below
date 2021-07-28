using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    Transform cam;

    [SerializeField]
    float speed = 6f;

    //Jump Affectors
    [SerializeField]
    float gravity = 9.8f;
    [SerializeField]
    float jumpSpeed = 6;
    //Look Affectors
    [SerializeField]
    float turnSmoothTime = 0.1f;
    Vector3 moveDirection;
    [SerializeField]
    GameObject GroundCheck;
    float turnSmoothVelocity;

    float directionY;

    //Dash Affectors
    [SerializeField]
    float dashSpeed = 1f;
    [SerializeField]
    float dashDecrease = 1f;
    [SerializeField]
    float dashEnd = -.5f;
    float currentDash;
    bool isDashing;

    private void Start()
    {
        currentDash = dashEnd;
        isDashing = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDashing) {
            HorizontalControl();

            VerticalControl();
        }
            

        Dash();
        
    }

    public void HorizontalControl()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angel = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angel, 0f);

            moveDirection = Quaternion.Euler(0f, angel, 0f) * Vector3.forward;
            controller.Move((moveDirection.normalized * speed) * Time.deltaTime);
        }
    }

    public void VerticalControl()
    {
        if (GroundCheck.GetComponent<GroundCheck>().IsGrounded())
        {
            directionY = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                directionY = jumpSpeed;
            }
        }

        directionY -= gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, directionY, 0f));
    }

    void Dash()
    {
        Debug.Log(isDashing);
        if (Input.GetButtonDown("Fire1") && currentDash <= dashEnd)
        {
            Debug.Log("Fire1 pressed");
            currentDash = dashSpeed;
            isDashing = true;
        }
        currentDash -= dashDecrease * Time.deltaTime;
        if(currentDash < 0)
        {
            isDashing = false;
        }
        if (isDashing)
        {
            controller.Move(moveDirection.normalized * currentDash);
        }
    }
}

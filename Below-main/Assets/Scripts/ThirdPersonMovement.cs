using System;
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
    [SerializeField]
    float jumpLeewayTime = .5f;
    float lastTimeTouchedGround;
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

    //WallJump Affectors
    [SerializeField]
    GameObject WallChecks;
    [SerializeField]
    float waitTime = 1;
    float timeLanded;
    bool wallJumpReady;
    bool isWallJumping;
    bool isWallDashing;
    int lastWallsTouched;
    float energyCharge = 1;
    [SerializeField]
    int maxEnergyCharge = 1;

    bool jump;
    bool dash;

    private void Start()
    {
        currentDash = dashEnd;
        isDashing = false;
        wallJumpReady = false;
        isWallJumping = false;
        isWallDashing = false;
        jump = false;
        dash = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(energyCharge);
        if (!(wallJumpReady || isWallJumping || isWallDashing))
        {
            if (!isDashing)
            {
                HorizontalControl();

                VerticalControl();
            }

            Dash();
        }

        WallJump();

        dash = false;
        jump = false;

    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            dash = true;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
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

            moveDirection = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
            controller.Move((moveDirection.normalized * speed) * Time.deltaTime);
        }
    }

    public void VerticalControl()
    {
        if (IsGrounded())
        {
            energyCharge = maxEnergyCharge;
            directionY = -gravity;
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
        if (dash && currentDash <= dashEnd && energyCharge >= 1)
        {
            dash = false;
            energyCharge -= 1;
            currentDash = dashSpeed;
            isDashing = true;
            directionY = 0;
        }
        currentDash -= dashDecrease * Time.deltaTime;
        if(currentDash < .5)
        {
            isDashing = false;
        }
        if (isDashing)
        {
            controller.Move(moveDirection.normalized * currentDash);
        }
    }

    void WallJump()
    {
        if(!IsGrounded() && 
            WallChecks.GetComponent<WallCheck>().IsTouchingWall() > 0 && energyCharge >= 1)
        {
            energyCharge -= 1;
            wallJumpReady = true;
            timeLanded = Time.time;
            lastWallsTouched = WallChecks.GetComponent<WallCheck>().IsTouchingWall();
            directionY = 0;
        }
        if(WallChecks.GetComponent<WallCheck>().IsTouchingWall() == 0)
        {
            wallJumpReady = false;
        }
        if (wallJumpReady)
        {
            if (Time.time - timeLanded > waitTime && !isWallJumping && !isWallDashing)
            {
                WallFall();
            }else if (jump)
            {
                wallJumpReady = false;
                isWallJumping = true;
                currentDash = .8f * dashSpeed;
            }else if (dash)
            {
                wallJumpReady = false;
                isWallDashing = true;
                currentDash = dashSpeed;
                energyCharge += 1;
            }
        }
        if (isWallDashing)
        {
            if(lastWallsTouched == 1)
            {
                currentDash -= dashDecrease * Time.deltaTime;
                controller.Move((-transform.forward + new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            }else if (lastWallsTouched == 2)
            {
                currentDash -= dashDecrease * Time.deltaTime;
                controller.Move(((-transform.forward + -transform.right).normalized 
                    + new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            }
            else if (lastWallsTouched == 3)
            {
                currentDash -= dashDecrease * Time.deltaTime;
                controller.Move(((-transform.right).normalized
                    + new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            }
            else if (lastWallsTouched == 4)
            {
                currentDash -= dashDecrease * Time.deltaTime;
                controller.Move(((transform.forward - transform.right).normalized
                    + new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            }
            else if (lastWallsTouched == 5)
            {
                currentDash -= dashDecrease * Time.deltaTime;
                controller.Move(((transform.forward).normalized
                    + new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            }
            else if (lastWallsTouched == 6)
            {
                currentDash -= dashDecrease * Time.deltaTime;
                controller.Move(((transform.right + transform.forward).normalized
                    + new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            }
            else if(lastWallsTouched == 7)
            {
                currentDash -= dashDecrease * Time.deltaTime;
                controller.Move(((transform.right).normalized
                    + new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            }
            else if (lastWallsTouched == 8)
            {
                currentDash -= dashDecrease * Time.deltaTime;
                controller.Move(((-transform.forward + transform.right).normalized
                    + new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            }
            if(currentDash <= .5)
            {
                isWallDashing = false;
            }
        }else if (isWallJumping)
        {
            currentDash -= dashDecrease * Time.deltaTime;
            controller.Move((new Vector3(0f, 1f, 0f)).normalized * dashSpeed);
            if (currentDash <= .5)
            {
                isWallJumping = false;
            }
        }
    }

    private void WallFall()
    {
        wallJumpReady = false;
    }

    private bool IsGrounded()
    {
        return GroundCheck.GetComponent<GroundCheck>().IsGrounded();
        if (GroundCheck.GetComponent<GroundCheck>().IsGrounded())
        {
            lastTimeTouchedGround = Time.time;
        }
        return (Time.time - lastTimeTouchedGround <= jumpLeewayTime);
    }
}

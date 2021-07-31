using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTheShoulderCharacterController : MonoBehaviour
{
    //Horizontal Movement Affectors
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    Transform cam;
    [SerializeField]
    float speed = 6f;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Controles the horizantal movement
    void Horizontal()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(direction.x, direction.z) *
                Mathf.Rad2Deg + cam.eulerAngles.y;

            moveDirection = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
            controller.Move((moveDirection.normalized * speed) * Time.deltaTime);
        }
    }
}

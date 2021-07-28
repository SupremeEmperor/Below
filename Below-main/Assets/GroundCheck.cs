using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    float groundDistance = 0.4f;
    [SerializeField]
    LayerMask groundMask;

    public bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position,groundDistance,groundMask);

    }

}

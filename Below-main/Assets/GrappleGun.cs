using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    [SerializeField]
    LineRenderer lr;
    Vector3 grapplePoint;
    [SerializeField]
    LayerMask GrappleLayer;
    [SerializeField]
    Transform gunTip, camera,player;
    [SerializeField]
    float maxDistance = 100;
    SpringJoint joint;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            StartGrapple();
        }
        if (Input.GetButtonUp("Fire3"))
        {
            EndGrapple();
        }
    }

    void StartGrapple()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(camera.position,camera.forward,out hit, maxDistance, GrappleLayer))
        {
            hit.transform.LookAt(player.transform);
            player.SetParent(hit.transform);
           
            player.gameObject.GetComponent<PlayerMovementScript>().enabled = false;
        }
    }

    void EndGrapple()
    {
        player.gameObject.GetComponent<PlayerMovementScript>().enabled = true;
        player.SetParent(null);
        player.rotation = new Quaternion(0, 0, 0, 0);

    }
}

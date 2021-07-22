using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnScript : MonoBehaviour
{
    [SerializeField]
    GameObject myCamera;
    [SerializeField]
    GameObject myPlayer;
    [SerializeField]
    float xoffput, yoffput, zoffput;
    List<GameObject> PossibleTargets;
    bool lockedOn = false;
    GameObject target;

    private void Start()
    {
        PossibleTargets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(PossibleTargets.Count == 0)
        {

        }else if(Input.GetAxis("LockOn") == 1 && lockedOn == false)
        {
            lockedOn = true;
            target = PossibleTargets[0];
            PossibleTargets.Remove(target);
            PossibleTargets.Add(target);
            target.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;

        }else if(lockedOn == true && Input.GetAxis("LockOn") == 1)
        {
            //look at the target
            myCamera.GetComponent<MouseLook>().SetLockedOn(true);;
            Vector3 temptarget = target.transform.position + new Vector3(xoffput, yoffput, zoffput);
            myPlayer.transform.LookAt(temptarget);

        }
        else if (lockedOn == true && Input.GetAxis("LockOn") == 0)
        {

            Quaternion look = myPlayer.transform.localRotation;
            lockedOn = false;
            target.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            myCamera.GetComponent<MouseLook>().SetLockedOn(false);
            myPlayer.transform.localRotation = look;
            myPlayer.transform.localRotation = new Quaternion(0, myPlayer.transform.localRotation.y,
                0, myPlayer.transform.localRotation.w);
            //myCamera.GetComponent<MouseLook>().setXRotation(Ex);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            PossibleTargets.Add(other.gameObject);

            //temp stuff 

           // other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

            //temp stuff end
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            PossibleTargets.Remove(other.gameObject);
            //other.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        
    }
}

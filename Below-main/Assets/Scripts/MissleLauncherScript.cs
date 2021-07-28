using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLauncherScript : MonoBehaviour
{
    [SerializeField]
    GameObject Missle;
    [SerializeField]
    float launchDelay = .5f;
    float lastLaunchTime;
    bool missleExists;
    GameObject currentMissle;



    // Start is called before the first frame update
    void Start()
    {
        lastLaunchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            this.transform.LookAt(other.transform);
            if (!missleExists && (Time.time - lastLaunchTime > launchDelay))
            {
                currentMissle = Instantiate(Missle,transform);
                currentMissle.transform.parent = null;
                currentMissle.GetComponent<MissleScript>().SetLauncher(this.gameObject);
                currentMissle.GetComponent<MissleScript>().SetTarget(other.gameObject);
                missleExists = true;
                lastLaunchTime = Time.time;
            }
        }
    }

    public void MissleDestroyed()
    {
        missleExists = false;
    }
}

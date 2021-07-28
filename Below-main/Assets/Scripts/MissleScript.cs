using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{
    [SerializeField]
    float fowardSpeed = 100;
    [SerializeField]
    float turnSpeed = 50;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float timeTillExplode = 10;
    float timeOfCreation;

    Rigidbody mainRB;
    Rigidbody childRB;

    GameObject Launcher;
    GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        mainRB = GetComponent<Rigidbody>();
        childRB = GetComponentInChildren<Rigidbody>();
        timeOfCreation = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mainRB.velocity = transform.forward * fowardSpeed;

        var targetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        mainRB.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed));
    }

    private void Update()
    {
        if(Time.time - timeOfCreation > timeTillExplode)
        {
            Explode();
        }
    }

    public void SetLauncher(GameObject L)
    {
        Launcher = L;
    }

    public void SetTarget(GameObject T)
    {
        Target = T;
    }

    public void Explode()
    {
        Launcher.GetComponent<MissleLauncherScript>().MissleDestroyed();
        Destroy(this.gameObject, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "MainCamera")
            Explode();
    }
}

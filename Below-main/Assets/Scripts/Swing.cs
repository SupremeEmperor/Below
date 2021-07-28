using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    Transform rotation1;
    [SerializeField]
    Transform rotation2;




    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.rotation.x );
        if(transform.localRotation.x >= rotation1.rotation.x || transform.localRotation.x <= rotation2.rotation.x)
        {
            speed = -speed;
        }
        transform.Rotate(speed, 0, 0);
    }


}

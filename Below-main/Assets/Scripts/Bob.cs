using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    [SerializeField]
    private float boblengthTime;
    [SerializeField]
    private float bobspeed;
    private float myTime;
    bool up;

    // Start is called before the first frame update
    void Start()
    {
        myTime = Time.time;
        up = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - myTime >= boblengthTime)
        {
            up = !up;
            myTime = Time.time;
        }
        int mult;
        if (up)
        {
            mult = 1;
        }
        else
        {
            mult = -1;
        }

        transform.Translate(Vector3.up * Time.deltaTime * mult * bobspeed);
    }
}

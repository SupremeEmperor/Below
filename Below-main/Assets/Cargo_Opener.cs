using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Cargo_Opener : MonoBehaviour
{
    [SerializeField]
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ani.SetTrigger("Open Door");
    }
}

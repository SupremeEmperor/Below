using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorScript : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Jump", false);

        float move = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", move);

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jump", true);
        }
    }
}

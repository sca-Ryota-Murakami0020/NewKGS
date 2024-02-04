using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookC : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current.xButton.wasPressedThisFrame)
        {
           anim.enabled = true;
           anim.SetBool("book",true);
        }
        if (Gamepad.current.yButton.wasPressedThisFrame)
        {
            anim.SetBool("book", false);
          
        }
    }

    public void OnFinish()
    {
        anim.enabled = false;
    }
}

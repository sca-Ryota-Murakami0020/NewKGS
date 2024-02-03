using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsekaiTruck : MonoBehaviour
{
    [SerializeField] PlayerC playerC;
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
        if(playerC.ALLGOAL) {
            anim.enabled = true;
        }
    }
}

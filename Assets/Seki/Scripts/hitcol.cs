using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitcol : MonoBehaviour
{
    [SerializeField] int count;
    [SerializeField] PlayerC pl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            Debug.Log("“–‚½‚Á‚½");
            //pl.SwitchPath(count);
        }
    }
}

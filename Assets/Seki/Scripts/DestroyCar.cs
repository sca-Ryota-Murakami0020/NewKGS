using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCar : MonoBehaviour
{
    bool arrive = false;
    public bool ARRIVE {
        set {
            this.arrive = value;
        }
        get {
            return this.arrive;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Car") {
            
            arrive = true;
        }
    }
}

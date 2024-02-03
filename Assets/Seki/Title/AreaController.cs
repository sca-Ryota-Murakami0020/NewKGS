using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    bool hit = false;

    public bool HIT {
        set {
            this.hit = value;
        }
        get {
            return this.hit;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hit);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("player")) {
            hit = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("player")) {
            hit = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("player")) {
            hit = false;
        }
    }
}

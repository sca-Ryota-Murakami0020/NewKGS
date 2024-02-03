using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
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

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            hit = true;
        }
    }
}

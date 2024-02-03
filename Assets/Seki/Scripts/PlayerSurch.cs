using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSurch : MonoBehaviour
{
    bool hitPlayer = false;
    public bool HITPLAYER {
        set {
            this.hitPlayer = value;
        }
        get {
            return this.hitPlayer;
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
        if(col.tag == "Player") {
            
            hitPlayer = true;
        }
    }
}

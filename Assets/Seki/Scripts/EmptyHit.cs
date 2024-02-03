using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyHit : MonoBehaviour
{
    [SerializeField] PlayerC playerC;
    bool wall = false;
    public bool WALLFLAG {
        set {
            this.wall = false;
        }
        get {
            return this.wall;
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

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Ground") {
            wall = true;
            //wall = other.gameObject;
            //parent.enabled = true;
            playerC.enabled = false;
        }
    }

    
}

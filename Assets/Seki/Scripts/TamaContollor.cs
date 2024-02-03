using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamaContollor : MonoBehaviour
{
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
              Destroy(this.gameObject);
        }
        if(col.tag == "Ground") {
            Destroy(this.gameObject);
        }
    }
}

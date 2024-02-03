using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKaidan : MonoBehaviour
{
    [SerializeField] KaidanC kaidanC;
    [SerializeField] int kaidanCount;
    BoxCollider boxCollider;
    [SerializeField] RallManager rall;
    // Start is called before the first frame update
    void Start() {
        boxCollider = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            Debug.Log("ƒqƒbƒg");

            //boxCollider.enabled = false;
            //StartCoroutine(WaitCol());
            //kaidanC.DESTPOINT = kaidanCount;
            ChangePos(rall.RALLCOUNT);
        }
    }

    void ChangePos(int y) {
        switch(y) {
            case 1:
                kaidanC.DESTPOINT = kaidanCount;
                break;
            case 2:
                kaidanC.DESTPOINT1 = kaidanCount;
                break;
            case 3:
                kaidanC.DESTPOINT2 = kaidanCount;
                break;
            case 4:
                kaidanC.DESTPOINT3 = kaidanCount;
                break;
        }
    }
}


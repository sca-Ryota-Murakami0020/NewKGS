using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatCol : MonoBehaviour
{
    [SerializeField] int colCount = 0;

    [SerializeField] PlayerInput playerInput;
    [SerializeField] CatController cat;
    BoxCollider bo;

    // Start is called before the first frame update
    void Start() {
        cat = cat.GetComponent<CatController>();
        bo = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {

            cat.DIRATIONCOUNT = colCount;
            //cat.STOPCAT = false;

            //playerInput.enabled = false;

        }
    }

    private void OnTriggerStay(Collider col) {
        if(col.tag == "Player") {
            if(colCount != 3) {
                cat.DIRATIONCOUNT = colCount;
                //cat.STOPCAT = false;

                //playerInput.enabled = false;
            }
        }
    }
}

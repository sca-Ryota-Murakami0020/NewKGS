using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StopSlope : MonoBehaviour
{
    [SerializeField] int slopeCount;
    [SerializeField] KaidanC kaidanC;
    [SerializeField] PlayerC playerC;
    [SerializeField] BoxCollider[] boxColliders;
    [SerializeField] BoxCollider[] stopCol;
    [SerializeField] RallManager rall;
    [SerializeField] PlayerInput player;
    [SerializeField] GameObject suberu;
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
            suberu.SetActive(false);
            if(slopeCount == 1) {

                kaidanC.UP = false;
                kaidanC.enabled = false;
                playerC.enabled = true;
                player.enabled = true;
                //boxColliders[1].enabled = true;
            }
            if(slopeCount == 2) {
                kaidanC.DOWN = false;
                kaidanC.enabled = false;
                playerC.enabled = true;
                player.enabled = true;
                //boxColliders[0].enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider col) {
        if(col.tag == "Player") {
            if(slopeCount == 1) {
               StartCoroutine(WaitCol());
                stopCol[1].enabled = true;
            }

            if(slopeCount == 2) {
                StartCoroutine(WaitCols());
                stopCol[0].enabled = true;
            }
        } 
    }

    IEnumerator WaitCol() {
        yield return new WaitForSeconds(1.0f);
        boxColliders[1].enabled = true;
    }

    IEnumerator WaitCols() {
        yield return new WaitForSeconds(1.0f);
        boxColliders[0].enabled = true;
    }
}

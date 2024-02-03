using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakudanCol : MonoBehaviour
{
    [SerializeField] BakudanController bakudanController;
    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            bakudanController.BAKUDACOUNT++;
            this.gameObject.SetActive(false);
        }
    }
}

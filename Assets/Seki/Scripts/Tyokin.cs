using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyokin : MonoBehaviour
{
    [SerializeField] MissionManager mission;
    BoxCollider bo;
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        bo = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mission.RADOMMISSIONCOUNT == 2 && mission.MISSIONCLEAR) {
            bo.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            gameManager.CurrentRemain++;
        }
    }
}

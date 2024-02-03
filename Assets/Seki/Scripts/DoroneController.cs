using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class DoroneController : MonoBehaviour
{
    [SerializeField] Transform[] Pos;
    NavMeshAgent agent;
    int destPoint = 0;
    [SerializeField] PauseManager pause;
    [SerializeField] GameObject player;
    [SerializeField] PlayerHit playerHit;
    PlayerC playerC;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerC = player.GetComponent<PlayerC>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause.PAUSE && !playerC.ALLGOAL) {
            if(!playerHit.HIT) { 
                if(!agent.pathPending && agent.remainingDistance < 0.5f) {
                    agent.speed = 3.5f;
                    GotoNextPoint();
                }
            } else {
                transform.DOLookAt(player.transform.position, 6.0f);
                playerHit.HIT = false;
            }
        } else {
            agent.speed = 0.0f;
        }
    }

    void GotoNextPoint() {
        if(Pos.Length == 0) {
            return;
        }

        //ene.SetBool("walk", false);
        agent.destination = Pos[destPoint].position;
         
        destPoint = (destPoint + 1) % Pos.Length;
    }
}

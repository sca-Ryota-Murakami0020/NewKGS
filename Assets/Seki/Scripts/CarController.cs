using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    private int destPoint = 0;
    [SerializeField] private Transform[] Pos;
    NavMeshAgent agent;
    [SerializeField] PlayerC player;
    [SerializeField] MissionManager mission;
    [SerializeField] PauseManager pause;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.FALLING && !pause.PAUSE && !mission.MISSIONFLAG) {
            agent.speed = 3.5f;
            if(!agent.pathPending && agent.remainingDistance < 0.3f) {
                
                GotoNextPoint();
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
        
        if(Pos.Length > destPoint) {
            agent.destination = Pos[destPoint].position;
            destPoint++;
        } else {
            destPoint = 0;
        }
    }
}

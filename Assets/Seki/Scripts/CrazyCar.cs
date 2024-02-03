using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrazyCar : MonoBehaviour
{
    [SerializeField] private Transform[] Pos;
    [SerializeField] MissionManager missionManager;
    private int destPoint = 0;
    NavMeshAgent agent;
    bool stop = false;
    float d;
    [SerializeField] PauseManager pause;
    [SerializeField] PlayerC player;
 
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }
    
    // Update is called once per frame
    void Update() {
        if(!stop  && !player.FALLING && !pause.PAUSE && !missionManager.MISSIONFLAG) {//
            if(!agent.pathPending && agent.remainingDistance < 0.3f) {
                GotoNextPoint();
            }
        }
        
    }

    void GotoNextPoint() {
        if(Pos.Length == 0) {
            return;
        }
        if(Pos.Length != destPoint) {
            //ene.SetBool("walk", false);
            agent.destination = Pos[destPoint].position;
        
            destPoint++;
        }
        
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "syata") {
            agent.speed = 0f;
            stop = true;
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            //ミッションの達成ポイントを加算する
            missionManager.MISSIONVALUE[missionManager.RADOMMISSIONCOUNT]++;
            missionManager.KeyActive(missionManager.RADOMMISSIONCOUNT);
            missionManager.MiSSIONCOUNT++;
        }
    }
}

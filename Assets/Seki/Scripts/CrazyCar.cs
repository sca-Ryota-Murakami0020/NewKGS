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
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject gameOverThings;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }
    
    // Update is called once per frame
    void Update() {
        if(!stop  && !player.FALLING && !pause.PAUSE && !missionManager.MISSIONFLAG) {//
            agent.speed = 2.5f;
            if(!agent.pathPending && agent.remainingDistance < 0.3f) {
                GotoNextPoint();
            }
        }
        else if(missionManager.MISSIONFLAG) {
            agent.speed = 0.0f;
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
        else if(destPoint == Pos.Length) {
            gameManager.GAMEOVER = true;
            gameManager.CurrentRemain = -1;
            gameOverThings.SetActive(false);
            player.FALLING = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "syata") {
            agent.speed = 0f;
            stop = true;
            other.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            //ミッションの達成ポイントを加算する
            missionManager.MISSIONVALUE[missionManager.RADOMMISSIONCOUNT]++;
            missionManager.KeyActive(missionManager.RADOMMISSIONCOUNT);
            missionManager.MiSSIONCOUNT++;
        }
    }
}

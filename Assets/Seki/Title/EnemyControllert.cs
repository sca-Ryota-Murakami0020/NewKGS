using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyControllert : MonoBehaviour
{
    //[SerializeField] AreaController bo;

    //�g�����v�������񂷂�|�C���g
    [SerializeField] private Transform[] points;
    //���񂷂�|�C���g����߂�ϐ�
    private int destPoint = 0;
    //NavMeshAgent�Ƃ���AI�@�\��g���ϐ�
    private NavMeshAgent agent;

    bool r = false;
    Animator anim;
    bool playerHit = false;
    [SerializeField] GameObject playerObj;
    bool enemyMove = false;
    PlayerC playerC;
    [SerializeField] PauseManager pause;
    [SerializeField] MissionManager mission;
    [SerializeField] BoxCollider hand;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //bo = bo.GetComponent<AreaController>();
        //destPoint = Random.Range(0, points.Length);
        anim = this.GetComponent<Animator>();
        playerC = playerObj.GetComponent<PlayerC>();
        hand.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHit && enemyMove) {

            transform.DOLookAt(playerObj.transform.position, 1.0f);
            playerHit = false;
        }
        if(mission.MISSIONFLAG || pause.PAUSE || enemyMove) { //
            agent.speed = 0f;
        } else {
            agent.speed = 3.5f;
            if(!playerHit && !enemyMove) {
                if(!agent.pathPending && agent.remainingDistance < 0.5f) {
                    GotoNextPoint();
                }
            }
        }
        if(playerC.FALLING) {
            enemyMove = false;
        }

    }

    IEnumerator WaitAttack() {
        yield return new WaitForSeconds(1.0f);
        playerHit = false;
    }

    //�p�j����֐�
    void GotoNextPoint() {
       
        if(points.Length == 0) {
            return;
        }

        //ene.SetBool("walk", false);
        if(destPoint < 4) {
            agent.destination = points[destPoint].position;
            destPoint++;
        }
        else{
            destPoint = 0;
        }
        //destPoint = (destPoint + 1) % points.Length;


    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            //if(//ここにプレイヤーの回避モーション中だったらの条件が入る)
            //回避モーション中だったら自分を消す
            //else
            playerHit = true;
            if(!pause.PAUSE || !mission.MISSIONFLAG) {
                hand.enabled = true;
                anim.SetTrigger("attack");
            }

            enemyMove = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        if(col.tag == "Player") {
            enemyMove = false;
            hand.enabled = false;
        }
    }
}

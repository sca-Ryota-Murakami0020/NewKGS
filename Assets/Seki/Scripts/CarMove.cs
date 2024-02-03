using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CarMove : MonoBehaviour
{
    [SerializeField] GameObject DangerImage;
    [SerializeField] PlayerSurch playersurch;
    [SerializeField] BoxCollider childBox;
    private NavMeshAgent agent;

    [SerializeField] private Transform[] points;
    private int destPoint = 0;

    Vector3 myPos;
    DestroyCar des;
    
    bool carMove = false;
    [SerializeField] BoxCollider myBox;
    int co = 0;
    [SerializeField] PauseManager pause;
    [SerializeField] MissionManager mission;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.0f;
        des = GetComponentInParent<DestroyCar>();
        myPos = this.transform.position;
        //agent = GetComponent<NavMeshAgent>();
        //agent.speed = 0f;
        DangerImage.SetActive(false);
        playersurch = playersurch.GetComponent<PlayerSurch>();
        childBox = childBox.GetComponent<BoxCollider>();
    }

    bool enter = false;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("プレイヤーヒット!!"+ playersurch.HITPLAYER);
        if(!pause.PAUSE && !mission.MISSIONFLAG ) {//&& carMove
            if(playersurch.HITPLAYER) {
                myBox.isTrigger = true;
                speed = 8.0f;
                Move(speed);
                childBox.enabled = false;
            } else {
                speed = 0.0f;
                myBox.isTrigger = false;
            }
        }
        if(this.transform.position == points[0].transform.position) {
            playersurch.HITPLAYER = false;
            this.transform.position = myPos;
            StartCoroutine(WaitCol());
            myBox.isTrigger = false;
            speed = 0.0f;
            
           
        }
    }

    private void Move(float speed) {
        // 移動量を計算
        var delta = this.transform.position - points[0].transform.position;

        // 静止している状態だと、進行方向を特定できないため回転しない
        if(delta == Vector3.zero)
            return;

        /*進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // オブジェクトの回転に反映
        this.transform.rotation = rotation;
        */

        float step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, points[0].transform.position, step);

    }

    IEnumerator WaitCol() {
        yield return new WaitForSeconds(3.0f);
        childBox.enabled = true;
    }
}

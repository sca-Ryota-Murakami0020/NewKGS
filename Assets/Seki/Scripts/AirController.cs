using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirController : MonoBehaviour
{
    [SerializeField] GameObject Pos;
    [Header("飛行機スピード"), SerializeField] float speed;
    public float AIRSPEED {
        set {
            this.speed = value;
        }
        get {
            return this.speed;
        }
    }
    bool hit = false;

    int hitCount = 0;
    public int HITCOUNT {
        set {
            this.hitCount = value;
        }
        get {
            return this.hitCount;
        }
    }

    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerC player;
    [SerializeField] GameObject missimage;
    [SerializeField] GameObject[] missIcon;
    [SerializeField] PauseManager pause;
    [SerializeField] MissionManager missionManager;
    //ここに爆発のエフェクトを用意

    bool air = false;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(!pause.PAUSE && !player.FALLING && !missionManager.MISSIONFLAG) {

        
        if(!hit) {
            var delta = this.transform.position - Pos.transform.position;

            // 静止している状態だと、進行方向を特定できないため回転しない
            if(delta == Vector3.zero)
                return;

            // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
            //var rotation = Quaternion.LookRotation(delta, Vector3.up);

            // オブジェクトの回転に反映
            //this.transform.rotation = rotation;


            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, Pos.transform.position, step);
        } else {
            for(int i = 0; i < missIcon.Length; i++) {
                missIcon[i].SetActive(false);
            }

            missimage.SetActive(false);
        }
        }

        if(hitCount >= 3 && !air) {
            
            //ここに爆発のエフェクトを出す
            StartCoroutine(WaitActive());
            air = true;
        }
    }

    IEnumerator WaitActive() {
        yield return null;
        missionManager.MISSIONVALUE[missionManager.RADOMMISSIONCOUNT] = 1;
        missionManager.KeyActive(missionManager.RADOMMISSIONCOUNT);
        missionManager.MiSSIONCOUNT++;
        this.gameObject.SetActive(false);
    }
    

    private void OnCollisionEnter(Collision collision) {    
        if(collision.gameObject.tag == "air") {
            hit = true;
            gameManager.GAMEOVER = true;
            gameManager.CurrentRemain = -1;
            
            player.FALLING = true;
            Debug.Log("HITT");
        }
        
    }
}

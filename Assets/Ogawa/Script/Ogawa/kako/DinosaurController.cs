using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurController : MonoBehaviour
{
    [SerializeField] Transform startPoint;       //恐竜の生成位置
    [SerializeField] Transform endPoint;         //恐竜の移動する位置
    [SerializeField] Transform bootPoint;        //ここにプレイヤーが侵入したら恐竜を動かす

    [SerializeField] GameObject player;

    private float elapsedTime = 0f;              //経過時間
    [SerializeField] float moveSpeed;        //startPosからendPosの移動にかける時間

    [SerializeField] float genelateDistance = 0.5f;//何秒に1体生成するか
    private float distanceCount = 0f;    //カウント

    void Start()
    {
        startPoint.position = this.transform.position;
    }

    private void Update()
    {
        
        
        if (player.transform.position.z >= bootPoint.position.z )
        {
            distanceCount+=Time.deltaTime;
            if (genelateDistance < distanceCount)
            {
                Spawn();
                distanceCount = 0f;
            }
            if(once == false)
            {
                Move();
            }

        }

    }

    bool once = true;
    int count = 0;
    void Spawn()
    {
        //恐竜を生成
        if (once)
        {
            Instantiate(this, startPoint.position, Quaternion.Euler(0f, 90f, 0f));
            once = false;
            count++;
        }


        
    }
    void Move()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < moveSpeed)
        {
            float t = elapsedTime / moveSpeed;
            this.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);

        }
        else
        {
            // 移動完了時の処理
            this.gameObject.SetActive(false);

        }
    }
}

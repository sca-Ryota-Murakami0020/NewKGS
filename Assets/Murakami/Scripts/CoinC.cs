using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinC : MonoBehaviour
{
    [Header("コインの価値"),SerializeField] private int coinPrice;
    [Header("スコア"),SerializeField] private int getScore;
    [Header("エフェクト"), SerializeField] private ParticleSystem partical;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Material[] cainMaterial;
    [SerializeField] private Renderer coinRenderer;
    //コインのメッシュ
    [SerializeField] private MeshRenderer mesh;

    //プレイヤーとコインの距離
    private float playerDistance = 0.0f;
    //プレイヤー
    private GameObject playerObject;
    //プレイヤーの座標
    private Vector3 playerPosition = Vector3.zero;
    //プレイヤーに向かう速度
    private float _speed = 2.0f;
    //元々の大きさ
    private Vector3 defScale;
    //縮小中のスケール倍率
    private Vector3 currentScaleMag;
    //初期位置
    private Vector3 defPos;
    //プレイヤーの追跡を行う
    private bool chasePlayer;
    //PlayerC
    private PlayerC playerC;
    //大きさの最小値
    private Vector3 minScale;

    private GameManager gameManager;
    MissionManager mission;

    void Start()
    {
        SelectMaterial();
        mission = GameObject.Find("MissionManager").GetComponent<MissionManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerObject = GameObject.Find("PlayerModel");
        playerC = playerObject.GetComponent<PlayerC>();
        defScale = this.transform.localScale;
        defPos = this.transform.position;
        currentScaleMag = defScale;
        playerPosition = playerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(mesh.enabled)
        {
            CheckPlayerDistance();
            if (playerDistance <= 1.7f)
            {
                //Debug.Log("関数取得");
                chasePlayer = true;
            }

            if (chasePlayer)
            {
                MoveCoin();
            }

            //プレイヤーが回収中にコインの方向に移動していたら
            if (playerDistance <= 0.1f)
            {
                GetCoin();
            }
        }
    }

    //色選択
    private void SelectMaterial()
    {
        switch(coinPrice)
        {
            case 10:
                this.coinRenderer.material = cainMaterial[0];
                break;
            case 100:
                this.coinRenderer.material = cainMaterial[1];
                break;
            case 500:
                this.coinRenderer.material = cainMaterial[2];
                break;
        }
    }

    //プレイヤーとの距離を測定する
    private void CheckPlayerDistance()
    {
        playerPosition = playerObject.transform.position;
        playerDistance = Vector3.Distance(playerPosition, this.transform.localPosition);
    }

    //近づいたプレイヤーの方向へ移動する
    private void MoveCoin()
    {
        var target = playerPosition;
        var distance = (_speed * Time.deltaTime) / playerDistance;
        this.transform.position = Vector3.Lerp(this.transform.position,target,distance);
        currentScaleMag = new Vector3(currentScaleMag.x - 2.0f, currentScaleMag.y, currentScaleMag.z - 2.0f);
        //縮小が完了したらプレイヤーの位置に移動する
        if((currentScaleMag.x <= 2.0f && currentScaleMag.z <= 2.0f) ||
            (currentScaleMag.x <= 2.0f || currentScaleMag.z <= 2.0f) ||
            (this.transform.localScale.x <= 0.1 || this.transform.localScale.z <= 0.1f))
        {
            currentScaleMag.x = 0.0f;
            currentScaleMag.z = 0.0f;
            this.transform.position = playerPosition;
        }
        this.transform.localScale = currentScaleMag;
    }

    //コインの獲得
    private void GetCoin()
    {
        this.mesh.enabled = false;
        chasePlayer = false;
        gameManager.AddCoins(coinPrice, getScore);
        gameManager.AddActiveCoin(this.gameObject);
        //エフェクト再生
        ParticleSystem newPar = Instantiate(partical);
        newPar.transform.position = playerC.PopObject.transform.position;
        newPar.Play();
        if (mission.RADOMMISSIONCOUNT == 2)
        {
            mission.MISSIONVALUE[mission.RADOMMISSIONCOUNT] += coinPrice;
        }
        this.transform.localScale = defScale;
        this.transform.localPosition = defPos;
        //this.gameObject.SetActive(false);
    }
}

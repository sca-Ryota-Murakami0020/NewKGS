using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤーの初期値を設定する(完全保存用。関数は入れない)
public class PlayerManager : MonoBehaviour
{
    //プレイヤーの残機初期値
    [SerializeField] private int defPlayerRemain;

    //プレイヤーの残機
    private int currentPlayerRemain;
    //プレイヤースクリプト
    private PlayerC playerC;
    //ステージ３用プレイヤースクリプト
    private RunOnlyPlayerC rPlayerC;
    //難易度のレベル
    private int gameLevel = 0;
    //プレイヤーのデフォルトのスピード倍率
    private float playerSpeedMag = 1.0f;
    //プレイヤーのデフォルトのジャンプ力
    private float playerJumpMag = 1.0f;
    //インスタンス
    private PlayerManager instance;

    //プロパティ
    public int ManagerRemain
    {
        get { return this.currentPlayerRemain;}
        set { this.currentPlayerRemain = value;}
    }
    public int GameLevel
    {
        get { return this.gameLevel;}
        set { this.gameLevel = value;}
    }
    public float DefSpeedMag
    {
        get { return this.playerSpeedMag;}
        set { this.playerSpeedMag = value;}
    }
    public float DefJumpMag
    {
        get { return this.playerJumpMag;}
        set { this.playerJumpMag = value;}
    }

    private void Awake()
    {
        currentPlayerRemain = defPlayerRemain;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
}

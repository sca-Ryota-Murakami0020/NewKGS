using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region//タイム関係
    [Header("ステージの制限時間"), SerializeField]
    private int stageCount;
    [Header("使用する画像"), SerializeField]
    private Sprite[] numberImage;
    [Header("タイム用Image"), SerializeField]
    private Image[] timeImage;
    #endregion

    #region//スコア関係
    [SerializeField]
    private TMP_Text scoreText;
    [Header("連続獲得を許容する時間（F）"), SerializeField]
    private float maxGetCount;
    #endregion

    #region//残機関係
    [SerializeField]
    private TMP_Text remainText;
    //実行中の残機
    private int currentRemain = 0;
    //残機用のアニメーション
    [SerializeField]
    private Animator remainAni;
    //ステージ間で残機を管理しているもの
    private PlayerManager playerManager = null;
    #endregion

    #region//所持金関係
    [SerializeField]
    private TMP_Text coinText;
    [Header("残機を増やす時の所持金額"), SerializeField]
    private int maxCoins;
    //所持金
    private int getCoins = 0;
    public int GETCOIN {
        set {
            this.getCoins = value;
        }
        get {
            return this.getCoins;
        }
    }
    //コイン用のアニメーション
    [SerializeField]
    private Animator coinAni;

    //管理しているコインのオブジェクト
    private Queue<GameObject> activeCoins = new Queue<GameObject>();
    #endregion

    //上位５名分＋挑戦した際のスコア
    private int[] rankingScore; 
    //今回の難易度
    private int difLevel = 0;
    //今回のスコア
    private int gameScore;
    //連続で獲得したコインの数
    private int getCoinCount = 0;
    //連続獲得可能時間の演算用変数
    private float canGetCoinsTime = 0.0f;
    //連続で獲得可能フラグ
    private bool canGetCoins = false;
    //選択された縛り
    private string[] selectBinding;
    //ポーズ中
    private bool stopGame = false;
    //プレイヤーの動きを制限する（カメラが追わないとか）
    //private bool dontMove = false;
    //実行中の経過時間
    private float currentGameTime = 0.0f;
    


    [SerializeField] GameObject playerObject;
    PlayerC player;
    PlayerInput playerInput;

    [SerializeField] Image PausePanel;
    float fadeSpeed = 0.02f;
    float P_red, P_green, P_blue, P_alfa;

    [SerializeField] PauseManager pause;

    [SerializeField] MissionManager mission;

    Vector3 dessPosition;

    public Vector3 DESSPOS {
        set {
            this.dessPosition = value;
        }
        get {
            return this.dessPosition;
        }
    }
    bool fadeIn = false;

    bool GameOver = false;
    public bool GAMEOVER {
        set {
            this.GameOver = value;
        }
        get {
            return this.GameOver;
        }
    }
    [SerializeField] MeshCollider plane;
    [SerializeField] GameObject gameOverThings;
    [SerializeField] GameObject gameClearThings;

    [SerializeField] GameObject zankiIocn;
    Text zankiIconText;
    [SerializeField] GameObject missText;
    CharacterController _characterController;
    
    [SerializeField] GameObject keyActiveObj;
    [SerializeField] GameObject gameOverplayer;
    [SerializeField] Canvas stageCanvas;
    //[SerializeField] CinemachineVirtualCamera cinema;
    #region//プロパティ
    public string[] SelectBinding
    {
        get { return this.selectBinding;}
        set { this.selectBinding = value;}
    }

    public int CurrentRemain
    {
        get { return this.currentRemain;}
        set { this.currentRemain = value;}
    }

    public int StageCount
    {
        get { return this.stageCount;}
    }

    public int GetScore
    {
        get { return this.gameScore;}
    }

    public int DifLevel
    {
        get { return this.difLevel;}
    }

    public Queue<GameObject> ActiveCoinObject
    {
        get { return this.activeCoins;}
        set { this.activeCoins = value;}
    }
    #endregion

    void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        stageCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        keyActiveObj.SetActive(true);
        _characterController = playerObject.GetComponent<CharacterController>();
        zankiIconText = zankiIocn.GetComponentInChildren<Text>();
        gameOverThings.SetActive(false);
        gameClearThings.SetActive(false);
             
        player = playerObject.GetComponent<PlayerC>();
        playerInput = playerObject.GetComponent<PlayerInput>();
        mission = mission.GetComponent<MissionManager>();
        pause = pause.GetComponent<PauseManager>();
        PausePanel = PausePanel.GetComponent<Image>();
        P_red = PausePanel.color.r;
        P_green = PausePanel.color.g;
        P_blue = PausePanel.color.b;
        P_alfa = PausePanel.color.a;
        dessPosition = player.transform.position;
        currentRemain = playerManager.ManagerRemain;

        //テキスト関係の初期化
        scoreText.SetText("Score:{0}",gameScore);
        remainText.SetText("X {0}",currentRemain);
        coinText.SetText("X {0}", getCoins);

    }

    // Update is called once per frame
    void Update()
    {

        if(player.KEYCOUNT >= 3) {
            keyActiveObj.SetActive(false);
        }

        //Debug.Log(dessPosition);
        if(GameOver) {
            stageCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            //GameOver = true;
            //cinema.enabled = false;
            GameOverActive();
        }
        if(canGetCoins)
        {
            CountSuccession();
        }
        if(!pause.PAUSE && !player.ALLGOAL && !player.FALLING) {
            //cinema.enabled = true;
        } else {
            //cinema.enabled = false;
        }

        if(!pause.PAUSE && !mission.MISSIONFLAG && !player.ALLGOAL && !GameOver)
        {
            CountGameTime();
        }
    }

    void GameClearActive() {
        player.enabled = false;
        
        gameClearThings.SetActive(true);
    }

    IEnumerator WaitGameClearActive() {
        yield return new WaitForSeconds(7.0f);
        GameClearActive();
    }

    void GameOverActive() {
        gameOverThings.SetActive(true);
        gameOverplayer.SetActive(true);
    }

    private void FixedUpdate() {
        if(!fadeIn && (player.FALLING || player.ALLGOAL)) {
            FadeOut();
        }
    }
    public void NameChange() {
        scoreText.SetText("Goal");
    }

    void FadeOut() {
        if(player.FALLING) {
            stageCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            if (!player.ALLGOAL) {
                StartCoroutine(WaitInoti());
            }

            if(P_alfa < 1.0f) {
                P_alfa += fadeSpeed;
                SetAlpha();
            } else if(P_alfa >= 1.0f) {
                fadeIn = true;

            }
        }
        if(player.ALLGOAL) {
            if(P_alfa < 1.0f) {
                P_alfa += fadeSpeed;
                SetAlpha();
            }
            else if(P_alfa >= 1.0f) {
                TitleManager.sceneName = "MojiHyouji";
                SceneManager.LoadScene("LoadScene");
            }
        }

    }

    IEnumerator WaitInoti() {
        if(currentRemain != 0) {
            missText.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            for(int i = 0; i < activeCoins.Count; i++)
            {
                var objects = activeCoins.Dequeue();
                MeshRenderer mesh = objects.GetComponent<MeshRenderer>();
                mesh.enabled = true;
            }
            
            plane.enabled = true;
            
            RevivePlayer();

            missText.SetActive(false);
            zankiIocn.SetActive(true);
            zankiIocn.SetActive(true);
            zankiIconText.text = "×" + currentRemain;
            List<IEnumerator> ie = new List<IEnumerator>();
            ie.Add(WaitU());
            foreach(IEnumerator item in ie) {
                StartCoroutine(item);

                yield return item.Current;// <===ここが重要
            }
            yield return null;
        } else {
            GameOver = true;
        }
    }

    bool remain = false;

    IEnumerator WaitU() {
        yield return new WaitForSeconds(1.0f);
        if (!remain) {
            //playerManager.ManagerRemain--;
            currentRemain--;
            zankiIconText.text = "×" + currentRemain;
            remain = true;
        }
        _characterController.enabled = true;
        player.FALLING = false;
        playerInput.enabled = true;

        if(!GameOver) {

            List<IEnumerator> ie = new List<IEnumerator>();
            ie.Add(WaitFadeIn());
            foreach(IEnumerator item in ie) {
                StartCoroutine(item);
                yield return item.Current;// <===ここが重要
            }
            yield return null;
        } else {
            //GameOverActive();
            //g = true;
            yield break;
        }
    }

    IEnumerator WaitFadeIn() {
        yield return new WaitForSeconds(1.0f);

        FadeIn();
    }

    void FadeIn() {
        zankiIocn.SetActive(false);

        if(P_alfa > 0.0f) {
            P_alfa -= fadeSpeed;
            SetAlpha();
        }

        if(P_alfa <= 0.0f) {
            stageCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            fadeIn = false;
            remain = false;
        }
    }

    void SetAlpha() {
        PausePanel.color = new Color(P_red, P_green, P_blue, P_alfa);
    }

    public void AddActiveCoin(GameObject coin)
    {
        activeCoins.Enqueue(coin);
    }

    int rage = 100;
    //ゲーム時間計算
    private void CountGameTime()
    {
        //時間計測
        currentGameTime += Time.deltaTime;
        if(currentGameTime >= 1.0f)
        {
            stageCount -= 1;
            currentGameTime = 0.0f;
        }
        timeImage[0].sprite = numberImage[stageCount / rage];
        timeImage[1].sprite = numberImage[(stageCount % rage) / 10];
        timeImage[2].sprite = numberImage[stageCount % (rage / 10)];
    }

    //スコア計算
    public void AddScore(int score)
    {
        //現在時間計測を行っているなら時間をリセットする
        if(canGetCoins)
        {
            canGetCoinsTime = 0.0f;
        }
        else
        {
            canGetCoins = true;
        }
        //スコア加算
        var getScore = score * Mathf.Pow(2, getCoinCount);
        gameScore += (int)getScore;
        //ここでカウントを増加する（必ず１倍、２倍、４倍...という感じで計算したいから）
        getCoinCount++;
        scoreText.SetText("Score:{0}", gameScore);
    }

    //コインの加算
    public void AddCoins(int price, int score)
    {
        if(!coinAni.GetBool("ShowCoin")) coinAni.SetBool("ShowCoin",true);
        getCoins += price;
        AddScore(score);
        //所持金が一定値を超えていれば
        if (getCoins >= maxCoins)
        {
            //PlusRemain();
            if(!coinAni.GetBool("ShowRemain"))remainAni.SetBool("ShowRemain", true);
            UpdateRemain(1);
            getCoins -= maxCoins;
        }
        coinText.SetText("X {0}", getCoins);
    }

    //連続獲得可能時間の計算処理(獲得するスコアを2倍、４倍、８倍...とする処理)
    private void CountSuccession()
    {
        canGetCoinsTime += 0.02f;
        if(canGetCoinsTime > maxGetCount)
        {
            canGetCoins = false;
            getCoinCount = 0;
            //アニメーション管理
            StartCoroutine(ReSetCoinUI());
        }
    }

    /// <summary>
    /// 残機があった場合の生き返り関数
    /// </summary>
    /// <param name="recount">チェックポイントの数</param>
    public void RevivePlayer() {
        if(currentRemain != 0 && playerManager.ManagerRemain != 0) {
            playerObject.transform.position = dessPosition;
        }
    }

    //
    public void ClearStage()
    {
        stopGame = true;
    }

    //残機計算処理
    public void UpdateRemain(int count)
    {
        playerManager.ManagerRemain += count;
        currentRemain += count;
        if (!coinAni.GetBool("ShowRemain")) remainAni.SetBool("ShowRemain", true);
        remainText.SetText("X {0}", currentRemain);
        StartCoroutine("CountCloseRemain");
    }

    //残機減少時の演出
    public void Fade()
    {
        //fadeAni.SetTrigger("FadeIn");
        StartCoroutine(CountStartFadeOut());
    }

    //UIを戻す
    private IEnumerator CountCloseRemain()
    {
        yield return new WaitForSeconds(3);
        remainAni.SetBool("ShowRemain",false);
        remainAni.SetTrigger("CloseRemain");
    }

    private IEnumerator ReSetCoinUI()
    {
        yield return new WaitForSeconds(2);
        coinAni.SetBool("ShowCoin", false);
        coinAni.SetTrigger("CloseCoin");
    }

    //ゲーム再スタート
    private IEnumerator CountStartFadeOut()
    {
        yield return new WaitForSeconds(2);
        ///fadeAni.SetTrigger("FadeOut");
        //dontMove = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    //ミッションのスライドUI
    [SerializeField] GameObject missionUI;
    Animator mission;

    //ミッション内容のテキスト
    [SerializeField] GameObject[] missionText;

    //ミッションの示唆UI
    [SerializeField] GameObject[] missionImage;

    //ミッションのオブジェクト(風船とか)
    [SerializeField] GameObject[] missionObj;

    //ミッション内容表示テキストUIとAnimation
   [SerializeField] Animator[] missionAnim;
    [SerializeField] Text[] misionValueText;
    int[] missionValue = new int[5];
    public int[] MISSIONVALUE {
        set {
            this.missionValue = value;
        }
        get {
            return this.missionValue;
        }
    }
    int sousaCount = 0;
    [SerializeField]
    int[] missionMaxValue;// = new int[5]
    //[SerializeField] Transform[] missionObjPos;//ミッションのオブジェクトの座標

    //ミッションの進捗変数
    int missionCount = 0;
    public int MiSSIONCOUNT {
        set {
            this.missionCount = value;
        }
        get {
            return this.missionCount;
        }
    }

    //ミッション中のフラグ
    bool missionFlag = false;
    public bool MISSIONFLAG {
        set {
            this.missionFlag = value;
        }
        get {
            return this.missionFlag;
        }
    }

    //ミッション開始フラグ
    bool startMission = false;
    public bool STATMISSION {
        set {
            this.startMission = value;
        }
        get {
            return this.startMission;
        }
    }

    //Playerのオブジェクトとスクリプト
    [SerializeField] GameObject playerObj;
    PlayerInput player;
  
    //キーのオブジェクト
    [SerializeField] GameObject[] Key;


    //int randomMissionCount = -1;
    public int RADOMMISSIONCOUNT {
        set {
            this.ransu = value;
        }
        get {
            return this.ransu;
        }
    }

    List<int> numbers = new List<int>();
    int ransu = -1;

    PlayerC playerC;

    string stageName;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
        stageName = SceneManager.GetActiveScene().name;
        playerC = playerObj.GetComponent<PlayerC>();
        for (int i = 0; i < missionImage.Length; i++) {
            numbers.Add(i);
        }
        for(int i = 0; i < missionAnim.Length; i++) {
            missionAnim[i] = missionAnim[i].GetComponent<Animator>();
            missionAnim[i].enabled = false;
        }
        
        for(int i = 0; i < Key.Length; i++) {
            Key[i].SetActive(false);
        }
        player = playerObj.GetComponent<PlayerInput>();
       
        mission = missionUI.GetComponent<Animator>();
        mission.enabled = false;
        for(int i = 0; i < missionImage.Length; i++) {
            missionImage[i].SetActive(false);
        }

        for(int y = 0; y < missionText.Length; y++) {
            missionText[y].SetActive(false);
        }

        for(int p = 0; p < missionObj.Length; p++) {
            missionObj[p].SetActive(false);
        }
        //misionValueText[missionCount].text = missionValue[missionCount] + "/" + missionMaxValue[missionCount];
        Invoke("ActivationMission", 1.0f);
    }
    
    bool missionClear = false;
    public bool MISSIONCLEAR {
        set {
            this.missionClear = value;
        }
        get {
            return this.missionClear;
        }
    }

    bool ybutton = false;
    public bool YBUTTON {
        set {
            this.ybutton = value;
        }
        get {
            return this.ybutton;
        }
    }

    [SerializeField] GameObject cinema;

    // Update is called once per frame
    void Update()
    {
        if(!missionFlag && !missionClear && !ybutton) {
            if(Gamepad.current.yButton.wasPressedThisFrame) {
                sousaCount++;
            }
            if(ransu != -1)
            {
                if (sousaCount == 1)
                {
                    missionAnim[ransu].enabled = true;
                    missionAnim[ransu].SetBool("mission", true);
                }
                else if (sousaCount == 2)
                {
                    missionAnim[ransu].SetBool("mission", false);

                    sousaCount = 0;
                }
            }
        }
    
        if(missionCount >= 1 && !startMission && playerC.IsGrand) {
            //StartCoroutine(WaitMission());
            ActivationMission();
        }

        if(missionFlag) { 
        if(Gamepad.current.bButton.wasPressedThisFrame) {
            player.enabled = true;
            cinema.SetActive(true);
            mission.SetBool("book",true);
            StartCoroutine(NotUI());
        }
        }

        if(missionCount > 2) {
            //startMission = false;
            missionFlag = false;
            missionUI.SetActive(false);
          
        }

        if(!missionClear && ransu != -1) { 
            if(missionValue[ransu] >= missionMaxValue[ransu]) {
                misionValueText[ransu].text = "CLEAR!!";
                missionValue[ransu] = 0;
                missionClear = true;
                ybutton = true;
                   
                missionAnim[ransu].enabled = true;
                missionAnim[ransu].SetBool("mission", true);
            } else if(!missionClear){
                misionValueText[ransu].text = missionValue[ransu].ToString() + "/" + missionMaxValue[ransu].ToString();
            }
        }
        if(missionClear) {// && stageName == "Murakami"
            sousaCount = 0;
            if (ransu != 2)
            {
                missionObj[ransu].SetActive(false);
            }
            
            StartCoroutine(WaitMission());
            
        }

       
    }

    IEnumerator WaitMission() {
        
        yield return new WaitForSeconds(2.0f);
        
        missionAnim[ransu].SetBool("mission", false);
        
        yield return new WaitForSeconds(1.0f);
     
        missionAnim[ransu].enabled = false;
        
        missionClear = false;

    }
    int randomMissionCount;
    void ActivationMission() {
        
        if(numbers.Count != 0) {
            randomMissionCount = Random.Range(0, numbers.Count);
            ransu = numbers[randomMissionCount];
            numbers.RemoveAt(randomMissionCount);
        }

        if(missionCount != 3) {
            missionImage[ransu].SetActive(true);
            startMission = true;
            missionFlag = true;

        }

        StartCoroutine(DelayMission());
    }

    void MissionActive(int count) {
            cinema.SetActive(false);
            player.enabled = false;
            sound.PlayOneShot(music);
            missionUI.SetActive(true);
            mission.enabled = true;
        for(int u = 0; u < missionImage.Length; u++) {
            if(count == u) {
                missionImage[count].SetActive(false);
                missionText[count].SetActive(true);
                missionObj[count].SetActive(true);
            }
            
            else if(count != u){
                missionText[u].SetActive(false);
            }
        }
    }

    public void KeyActive(int count) {
        Key[count].SetActive(true);
    }

    IEnumerator DelayMission() {
        yield return new WaitForSeconds(1.0f);
        MissionActive(ransu);
    }

    IEnumerator NotUI() {
        yield return new WaitForSeconds(2.0f);
        mission.SetBool("book", false);
        mission.enabled = false;
        missionUI.SetActive(false);
        missionFlag = false;
    }
}

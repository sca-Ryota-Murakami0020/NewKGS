using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class KakoPauseManager : MonoBehaviour
{
    [SerializeField] RectTransform[] Point;
    [SerializeField] RectTransform myPos;
    bool pause = false;
    public bool PAUSE {
        set {
            this.pause = value;
        }
        get {
            return this.pause;
        }
    }

    int selectCount = 0;

    [SerializeField] GameObject PauseUI;
    [SerializeField] RawImage PausePanel;

    float alfa;//float red,green,blue
    float P_red, P_green, P_blue, P_alfa;

    float fadeSpeed = 0.06f;
    float P_fadeSpeed = 0.02f;
    bool isFadeFlag = false;

    bool check = false;
    bool playStart = false;

    [SerializeField] Text ScoreText;

    [SerializeField] GameObject RestartUI;
    bool restart = false;
    [SerializeField] RectTransform restartIcon;
    [SerializeField] RectTransform[] restartPos;
    string stageName;
    private PlayerManager playerManager = null;

    [SerializeField] GameObject[] line;
    [SerializeField] GameObject[] titleBack;

    [SerializeField] RunOnlyPlayerC player;
    bool load = false;
    public bool LOAD {
        set {
            this.load = false;
        }
        get {
            return this.load;
        }
    }
    [SerializeField] Rigidbody rbPlayer;
    [SerializeField] ThirdStageGM gameManager;
    [SerializeField] Text pauseZaznkiText;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < line.Length; i++) {
            line[i].SetActive(false);
            titleBack[i].SetActive(false);
        }

        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        RestartUI.SetActive(false);
        for(int i = 0; i < Point.Length; i++) {
            Point[i] = Point[i].GetComponent<RectTransform>();
        }

        PausePanel = PausePanel.GetComponent<RawImage>();
        PauseUI.SetActive(false);

        alfa = PausePanel.color.a;
        stageName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

        pauseZaznkiText.text = "×"+ gameManager.CURRENTREMAIN;
        //ポーズ中
        if(pause && !restart) {
            
            pauseIconMove();

            if(myPos.localPosition == Point[0].localPosition) {
                LineMove(0);
                if(Gamepad.current.bButton.isPressed) {
                    isFadeFlag = true;
                    check = true;
                    playStart = true;
                    
                }
            }

            if(myPos.localPosition == Point[1].localPosition) {
                LineMove(1);
                if(Gamepad.current.bButton.isPressed) {
                    StartCoroutine(WaitRestart(1));

                }
            }

            if(myPos.localPosition == Point[2].localPosition) {
                LineMove(0);
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    SousaUIContorller.stageClear = 0;
                    playerManager.ManagerRemain = 3;
                    
                    TitleManager.sceneName = "Masaki";
                    SceneManager.LoadScene("LoadScene");
                }
            }

        }

        //ポーズ中じゃない
        else if(!restart && player.ONGROUND) {
            if(Gamepad.current.startButton.isPressed) {
                rbPlayer.isKinematic = true;
                pause = true;
                isFadeFlag = true;
                check = false;
               
            }
        }

        if(restart) {
            RestartUI.SetActive(true);
            restartIconMove();
            if(restartIcon.localPosition == restartPos[0].localPosition) {
                TitleLineMove(0);
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    //playerManager.DEFRE = 3;
                    load = false;
                    TitleManager.sceneName = stageName;
                    SceneManager.LoadScene("LoadScene");
                }
            }
            if(restartIcon.localPosition == restartPos[1].localPosition) {
                TitleLineMove(1);
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    StartCoroutine(WaitRestart(2));
                }
            }
        }
    }

    void LineMove(int c) {
        for(int i = 0; i < line.Length; i++) {
            if(i == c) {
                line[c].SetActive(true);
                titleBack[c].SetActive(true);
            } else {
                line[i].SetActive(false);
                titleBack[i].SetActive(false);
            }
        }
    }

    void TitleLineMove(int c) {
        for(int i = 0; i < line.Length; i++) {
            if(i == c) {
                titleBack[c].SetActive(true);
            } else {
                titleBack[i].SetActive(false);
            }
        }
    }

    IEnumerator WaitU() {
        yield return new WaitForSeconds(1.0f);

    }

    IEnumerator WaitRestart(int c) {
        yield return new WaitForSeconds(0.3f);
        if(c == 1) {
            restart = true;
        } else if(c == 2) {
            restart = false;
            RestartUI.SetActive(false);
            restartIcon.localPosition = restartPos[0].localPosition;
        }
    }

    void restartIconMove() {
        if(Gamepad.current.leftStick.left.wasPressedThisFrame) {
            restartIcon.localPosition = restartPos[0].localPosition;
        }
        if(Gamepad.current.leftStick.right.wasPressedThisFrame) {
            restartIcon.localPosition = restartPos[1].localPosition;
        }
    }

    void pauseIconMove() {
        if(selectCount > 0) {
            if(Gamepad.current.leftStick.up.wasPressedThisFrame) {
                selectCount--;
                myPos.localPosition = Point[selectCount].localPosition;
            }
        }

        if(selectCount < 2) {
            if(Gamepad.current.leftStick.down.wasPressedThisFrame) {

                selectCount++;
                myPos.localPosition = Point[selectCount].localPosition;
            }

        }
    }

    private void FixedUpdate() {
        if(isFadeFlag) {
            StartFadeOut();
        } else if(!isFadeFlag && pause) {
            StartFadeIn();
        }
    }

    void StartFadeOut() {
        alfa += fadeSpeed;
        SetAlpha();
        if(alfa >= 1f) {
            StartFadeIn();
        }

    }

    void StartFadeIn() {
        if(alfa > 0f) {
            if(check) {
                if(alfa >= 0.55f) {
                    PauseUI.SetActive(false);
                }

            }
            if(!check) {

                PauseUI.SetActive(true);
            }
            alfa -= P_fadeSpeed;
            SetAlpha();
            isFadeFlag = false;
        }

        if(alfa <= 0f && playStart) {
            rbPlayer.isKinematic = false;
            pause = false;
            playStart = false;
        }

    }

    void SetAlpha() {
        PausePanel.color = new Color(255, 255, 255, alfa);
    }
}

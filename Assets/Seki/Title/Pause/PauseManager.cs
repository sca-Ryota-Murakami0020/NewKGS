using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseManager : MonoBehaviour
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

    float red,green,blue,alfa;
    float P_red, P_green, P_blue, P_alfa;

    float fadeSpeed = 0.04f;
    float P_fadeSpeed = 0.01f;
    bool isFadeFlag = false;

    bool check =false;
    bool playStart = false;

    [SerializeField] GameManager gameManager;

    [SerializeField] Text[] ScoreText;

    [SerializeField] MissionManager mission;
    [SerializeField] GameObject player;
    PlayerC playerC;
    PlayerInput playerInput;

    [SerializeField] Image[] KeyImage;

    [SerializeField] GameObject RestartUI;
    bool restart = false;
    [SerializeField] RectTransform restartIcon;
    [SerializeField] RectTransform[] restartPos;
    string stageName;
    private PlayerManager playerManager = null;
    [SerializeField] CinemachineInputProvider cinema;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        RestartUI.SetActive(false);
        for(int i = 0; i < Point.Length; i++) {
            Point[i] = Point[i].GetComponent<RectTransform>();
        }

        for(int u = 0; u < KeyImage.Length; u++) {
            KeyImage[u] = KeyImage[u].GetComponent<Image>();
            KeyImage[u].color = new Color(0f, 0f, 0f);
        }

        playerInput = player.GetComponent<PlayerInput>();
        playerC = player.GetComponent<PlayerC>();
        PausePanel = PausePanel.GetComponent<RawImage>();
        PauseUI.SetActive(false);
        red = PausePanel.color.r;
        green = PausePanel.color.g;
        blue = PausePanel.color.b;
        alfa = PausePanel.color.a;
        //alfa = 0f;
        stageName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

        ScoreText[0].text = "×" + gameManager.CurrentRemain.ToString();
        ScoreText[1].text = "×" + gameManager.GETCOIN.ToString();

        if(playerC.KEYCOUNT != 0) {
            KeyImage[playerC.KEYCOUNT - 1].color = new Color(255f, 255f, 255f);
        }

        //ポーズ中
        if(pause && !restart) {
            
            pauseIconMove();

            if(myPos.localPosition == Point[0].localPosition) {
                if(Gamepad.current.bButton.isPressed) {
                    isFadeFlag = true;
                    check = true;
                    playStart = true;
                    playerInput.enabled = true;
                    cinema.enabled = true;
                }
            }

            if(myPos.localPosition == Point[1].localPosition) {
                if(Gamepad.current.bButton.isPressed) {
                    StartCoroutine(WaitRestart(1));

                }
            }

            if(myPos.localPosition == Point[2].localPosition) {
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    SousaUIContorller.stageClear = 0;
                    playerManager.ManagerRemain = 3;
                    TitleManager.sceneName = "Masaki";
                    SceneManager.LoadScene("LoadScene");
                }
            }

        }
        //ポーズ中じゃない
        else if(!mission.MISSIONFLAG && !restart) {
            if(Gamepad.current.startButton.isPressed) {
                cinema.enabled = false;
                pause = true;
                isFadeFlag = true;
                check = false;
                playerInput.enabled = false;
            }
        }

        if(restart) {
            RestartUI.SetActive(true);
            restartIconMove();
            if(restartIcon.localPosition == restartPos[0].localPosition) {
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    //playerManager.DEFRE = 3;
                    TitleManager.sceneName = stageName;
                    SceneManager.LoadScene("LoadScene");
                }
            }
            if(restartIcon.localPosition == restartPos[1].localPosition) {
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    StartCoroutine(WaitRestart(2));
                }
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
        
            pause = false;
            playStart = false;
        }

    }

    void SetAlpha() {
        PausePanel.color = new Color(red, green, blue, alfa);
    }
}

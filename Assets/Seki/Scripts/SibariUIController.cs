using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SibariUIController : MonoBehaviour
{
    [SerializeField] Hontoni ho;
    int sibariCount = 0;
    RectTransform myPos;
    float jyougeSpeed = 134f;
    int sousaCount = 0;

    [SerializeField] RectTransform[] R_Pos;
    [SerializeField] RectTransform[] L_Pos;

    [SerializeField] RectTransform[] Pos;
    [SerializeField] GameObject[] BackImag;

    [SerializeField] GameObject honto;

    [SerializeField] RawImage Decied;

    [SerializeField] GameObject[] Icon;

    [SerializeField] GameObject[] revelText;

    [SerializeField] RawImage sibariWaku;

    bool placeDebath;

    [SerializeField] RectTransform DeciePos;
    Vector2 copyTransform;

    bool next;
    public bool NEXT {
        set {
            this.next = value;
        }
        get {
            return this.next;
        }
    }


    bool hon;
    SibariUIController myComp;

    int[] SentakuCount;
    public int[] SENTAKU {
        set {
            this.SentakuCount = value;
        }
        get {
            return this.SentakuCount;
        }
    }
    int placeCount;

    int revel;
    public int REVEL {
        set {
            this.revel = value;
        }
        get {
            return this.revel;
        }
    }

    [SerializeField] GameObject modeStage;
    [SerializeField] GameObject SibariKind;
    int right;
    [SerializeField] TitleManager titleManager;

    private PlayerManager playerManager;
    [SerializeField] StageSelectController stage;
    [SerializeField] Animator modeAnim;
    // Start is called before the first frame update
    void Start() {
        //stage.enabled = false;
        titleManager.SelectSetumei(2);
        SetApp();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    void SetApp() {
        placeDebath = false;
        next = false;
        hon = false;
        SentakuCount = new int[6];
        placeCount = 0;
        sousaCount = 0;
        revel = 0;
        right = 0;
        sibariCount = 0;
        sibariWaku = sibariWaku.GetComponent<RawImage>();
        sibariWaku.color = new Color32(255, 11, 11, 255);
        ho = ho.GetComponent<Hontoni>();
        ho.enabled = false;
        Decied = Decied.GetComponent<RawImage>();
        Decied.color = new Color(142, 142, 142, 255);
        honto.SetActive(false);
        for(int i = 0; i < BackImag.Length; i++) {
            BackImag[i].SetActive(false);
        }
        myPos = this.GetComponent<RectTransform>();
        Vector2 p = myPos.localPosition;
        p.y = 134f;
        p.x = -597f;
        myPos.localPosition = p;
        myComp = this.GetComponent<SibariUIController>();
        for(int i = 0; i < SentakuCount.Length; i++) {
            SentakuCount[i] = 0;
        }
        for(int i = 0; i < Icon.Length; i++) {
            Icon[i].SetActive(false);
        }

        for(int i = 0; i < revelText.Length; i++) {
            revelText[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(right);
        RevalWaku(revel);
        if(!next) {

            InputRightStick(sousaCount);
            InputLeftStick(sousaCount);


            InputDownStick(right);
            InputUpStick();


            if(myPos.localPosition == Pos[0].localPosition) {
                placeCount = 0;
                SentakuIcon(placeCount);
                if(Gamepad.current.bButton.wasPressedThisFrame) {

                    SentakuPoint(placeCount);
                }
            }
            if(myPos.localPosition == Pos[1].localPosition) {
                placeCount = 1;
                SentakuIcon(placeCount);
                if(Gamepad.current.bButton.wasPressedThisFrame) {

                    SentakuPoint(placeCount);
                }
            }
            if(myPos.localPosition == Pos[2].localPosition) {
                placeCount = 2;
                SentakuIcon(placeCount);
                if(Gamepad.current.bButton.wasPressedThisFrame) {

                    SentakuPoint(placeCount);
                }
            }
            if(myPos.localPosition == Pos[3].localPosition) {
                placeCount = 3;
                SentakuIcon(placeCount);
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    SentakuPoint(placeCount);
                }
            }
            if(myPos.localPosition == Pos[4].localPosition) {
                placeCount = 4;
                SentakuIcon(placeCount);
                if(Gamepad.current.bButton.wasPressedThisFrame) {

                    SentakuPoint(placeCount);
                }
            }
            if(myPos.localPosition == Pos[5].localPosition) {
                placeCount = 5;
                SentakuIcon(placeCount);
                if(Gamepad.current.bButton.wasPressedThisFrame) {

                    SentakuPoint(placeCount);
                }
            }
           
            if(myPos.localPosition == DeciePos.localPosition) {
                if(Gamepad.current.bButton.isPressed) {

                    hon = true;
                    next = true;
                }
            }
            /*
            if(hon) {
                honto.SetActive(true);
                hon = false;
                myComp.enabled = false;
                ho.enabled = true;
            }
            */
            if(sibariCount != 0) {
                Decied.color = new Color32(255, 255, 255, 255);

            } else {
                Decied.color = new Color32(142, 142, 142, 255);
            }
        }

        if(Gamepad.current.leftShoulder.wasReleasedThisFrame) {
            if(revel > 0) {
                revel--;
            }
        }
        if(Gamepad.current.rightShoulder.wasReleasedThisFrame) {
            if(revel < 2) {
                revel++;
            }
        }
        for(int i = 0; i < revelText.Length; i++) {
            if(revel == i) {
                revelText[i].SetActive(true);
            } else {
                revelText[i].SetActive(false);
            }
        }


        if(Gamepad.current.aButton.isPressed) {
            SetApp();
            myComp.enabled = false;
            stage.enabled = true;
            SibariKind.SetActive(false);
            modeStage.SetActive(true);
          
            modeAnim.SetBool("title",true);
        }
    }

    void SentakuPoint(int count) {
        SentakuCount[count]++;
        if(SentakuCount[count] == 2) {
            BackImag[count].SetActive(false);
            SentakuCount[count] = 0;
            sibariCount--;
        } else {
            BackImag[count].SetActive(true);
            sibariCount++;
        }
        copyTransform = myPos.localPosition;
    }

    void RevalWaku(int revel) {
        switch(revel) {
            case 0:
                sibariWaku.color = new Color32(255, 11, 11, 255);
                break;
            case 1:
                sibariWaku.color = new Color32(255, 242, 11, 255);
                break;
            case 2:
                sibariWaku.color = new Color32(11, 146, 255, 255);
                break;
        }
    }

    void SentakuIcon(int count) {
        for(int i = 0; i < Icon.Length; i++) {
            if(i == count) {
                Icon[i].SetActive(true);
            } else {
                Icon[i].SetActive(false);
            }
        }


    }

    /// <summary>
    /// スティックで上を押した関数
    /// </summary>
    void InputUpStick() {
        if(myPos.localPosition.y < 134f && myPos.localPosition != DeciePos.localPosition) {
            if(Gamepad.current.leftStick.up.wasPressedThisFrame) {
                Vector2 p = myPos.localPosition;
                if(sousaCount > -1) {
                    sousaCount--;
                }

                p.y += jyougeSpeed;

                myPos.localPosition = p;
            }

        }
    }

    /// <summary>
    /// スティックで下を押した関数
    /// </summary>
    /// <param name="right">右スティックを押した回数</param>
    void InputDownStick(int right) {
        if(right == 0) {

            if(myPos.localPosition.y > -268f && myPos.localPosition != DeciePos.localPosition) {
                if(Gamepad.current.leftStick.down.wasPressedThisFrame) {
                    Vector2 p = myPos.localPosition;
                    if(sousaCount < 3) {
                        sousaCount++;
                    }

                    if(placeDebath && sousaCount == 3) {
                        myPos.localPosition = L_Pos[2].localPosition;
                    } else {
                        p.y -= jyougeSpeed;
                    }

                    myPos.localPosition = p;

                }
            }
        } else if(right == 1) {
            if(myPos.localPosition.y > -134f && myPos.localPosition != DeciePos.localPosition) {
                if(Gamepad.current.leftStick.down.wasPressedThisFrame) {
                    Vector2 p = myPos.localPosition;
                    if(sousaCount < 3) {
                        sousaCount++;
                    }

                    if(placeDebath && sousaCount == 3) {
                        myPos.localPosition = L_Pos[2].localPosition;
                    } else {
                        p.y -= jyougeSpeed;
                    }

                    myPos.localPosition = p;

                }
            }
        }
    }

    void InputRightStick(int c) {
        if(Gamepad.current.leftStick.right.wasReleasedThisFrame) {
            if(sibariCount == 0 && right != 1) {
                right++;
            } else if(sibariCount > 0 && right != 2) {
                right++;
            }

            if(right == 2 && sibariCount >= 1) {
                if(myPos.localPosition != DeciePos.localPosition && myPos.localPosition.x == 96f) {

                    myPos.localPosition = DeciePos.localPosition;
                }

            } else {
                for(int i = 0; i < R_Pos.Length; i++) {
                    if(i == c && i != 3 && c != 3) {
                        myPos.localPosition = R_Pos[c].localPosition;
                    } else if(c == 3) {
                        myPos.localPosition = R_Pos[2].localPosition;
                    }
                }
            }
        }
    }

    void InputLeftStick(int c) {
        if(Gamepad.current.leftStick.left.wasReleasedThisFrame) {
            if(myPos.localPosition == DeciePos.localPosition && right == 2) {
                right = 1;
                if(c == 3) {
                    c = 2;
                }
                myPos.localPosition = R_Pos[c].localPosition;

            } else {
                if(right > 0 && myPos.localPosition != DeciePos.localPosition) {
                    right--;
                }
                for(int i = 0; i < L_Pos.Length; i++) {

                    if(i == c) {
                        myPos.localPosition = L_Pos[c].localPosition;
                    }
                }
            }
        }
    }
}

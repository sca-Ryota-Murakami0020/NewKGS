using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IconController : MonoBehaviour
{
    RectTransform my;
    [SerializeField] RectTransform[] Title;
    bool stageFlag = false;
    public bool STAGE {
        set {
            this.stageFlag = value;
        }
        get {
            return this.stageFlag;
        }
    }
    bool titlefade = false;
    public bool TITLEFADE {
        set { this.titlefade = value; }
        get { return this.titlefade; }
    }
    [SerializeField]
    GameObject[] underUi;
    [SerializeField] GameObject mode;
    Animator animator;
    [SerializeField] Animator parent;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < underUi.Length; i++) {
            underUi[i].SetActive(false);
        }
        my = this.GetComponent<RectTransform>();
        animator = this.GetComponent<Animator>();
        mode.SetActive(false);
        my.localPosition = Title[0].localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stageFlag) {
            IconPos();
        }
        


        if(my.localPosition == Title[0].localPosition) {
            UnderUI(0);
            if(Gamepad.current.bButton.wasReleasedThisFrame) {
                stageFlag = true;
                parent.SetBool("title", false);
            }
        }

        if(my.localPosition == Title[1].localPosition) {
            UnderUI(1);
            if(Gamepad.current.bButton.wasReleasedThisFrame) {
                //stageFlag = true;
                //Siabritukeru.SetActive(true);
            }
        }
        if(titlefade) {
            mode.SetActive(true);
            titlefade = false;
        }
    }



    /// <summary>
    /// É^ÉCÉgÉãÇÃIconÇìÆÇ©Ç∑ä÷êî
    /// </summary>
    void IconPos() {
        if(Gamepad.current.leftStick.right.wasReleasedThisFrame) {
            my.localPosition = Title[0].localPosition;
            
            animator.SetBool("Tyoku",false);
        }
        if(Gamepad.current.leftStick.left.wasReleasedThisFrame) {
            my.localPosition = Title[1].localPosition;

            animator.SetBool("Tyoku", true);
        }
    }

    void UnderUI(int c) {
        for(int i = 0; i < underUi.Length; i++) {
            if(i == c) {
                underUi[c].SetActive(true);
            } else {
                underUi[i].SetActive(false);
            }
        }
    }
}

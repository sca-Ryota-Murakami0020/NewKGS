using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


/// <summary>
/// これはモード選択を作るとなった時に使うスクリプト
/// </summary>
public class StageSelectController : MonoBehaviour
{
    RectTransform myPos;
    [SerializeField] RectTransform[] Pos;
   

    bool normal = false;
    public bool NORMAL {
        set {
         this.normal = value;
        }
       get {
           return this.normal;
      }
    }

    StageSelectController myScripts;
    [SerializeField] TitleManager title;

    public enum MODE
    {
        STORY = 0,
        CHALLENGE = 1,
        NULL = 2,
    }

    public static MODE mode;
    Animator animator;
    [SerializeField] Animator parent;
    
    [SerializeField] IconController icon;
    [SerializeField] GameObject playerImage;
    bool modeFlag;
    public bool MODEFLAG {
        set {
            this.modeFlag = value;
        }
        get {
            return this.modeFlag;
        }
    }

    [SerializeField] GameObject[] music;
    // Start is called before the first frame update
    void Start()
    {
        icon.enabled = false;
        parent.enabled = true;
        modeFlag = false;
        animator = GetComponent<Animator>();
        mode = MODE.NULL;
        myScripts = this.GetComponent<StageSelectController>();
        myScripts.enabled = true;
        //si = Siabritukeru.GetComponent<SibariTukeru>();
        //si.enabled = false;
        
        myPos = this.GetComponent<RectTransform>();
        for(int i = 0; i < Pos.Length; i++) {
            Pos[i] = Pos[i].GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!modeFlag) {
            StageIconMove();
            StageSelect();
        }
        
    }

    void StageIconMove() {
        if(Gamepad.current.leftStick.left.wasPressedThisFrame) {
            animator.SetTrigger("Tyoku");
            myPos.localPosition = Pos[0].localPosition;
           
        }

        if(Gamepad.current.leftStick.right.wasPressedThisFrame) {
            
            animator.SetTrigger("Tyoku");
            myPos.localPosition = Pos[1].localPosition;
        }
        if(Gamepad.current.aButton.wasPressedThisFrame)
        {
            normal = false;
            modeFlag = true;
            playerImage.SetActive(false);
            parent.SetBool("fade", true);
        }
    }

    void StageSelect() {
        if(myPos.localPosition == Pos[0].localPosition) {
            title.SelectSetumei(0);
            SoundMusic(0);
            if(Gamepad.current.bButton.wasPressedThisFrame) {
                mode = MODE.STORY;
                parent.SetBool("fade",true);
                normal = true;
            }
            
        }
        if(myPos.localPosition == Pos[1].localPosition) {
            title.SelectSetumei(1);
            SoundMusic(1);
            if(Gamepad.current.bButton.wasPressedThisFrame) {
                //normal = true;
                mode = MODE.CHALLENGE;
                parent.SetBool("fade", true);
                
                StartCoroutine(SibariActive());
            }
        }
    }

    IEnumerator SibariActive() {
        yield return new WaitForSeconds(0.5f);
        //si.enabled = true;
        //parent.enabled = false;
        //myScripts.enabled = false;
    }

    void SoundMusic(int c) {
        for(int i = 0; i < music.Length; i++) {
            if(i == c) {
                music[c].SetActive(true);
            } else {
                music[i].SetActive(false);
            }
        }
    }
}

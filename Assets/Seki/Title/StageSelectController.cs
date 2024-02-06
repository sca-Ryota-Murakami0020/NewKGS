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
    [SerializeField] GameObject Siabritukeru;
    SibariTukeru si;

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

    bool not = false;
    public bool NOT
    {
        set
        {
            this.not = value;
        }
        get
        {
            return this.not;
        }
    }

    public static MODE mode;
    Animator animator;
    [SerializeField] Animator parent;
    [SerializeField] GameObject ModeUi;
    [SerializeField] IconController icon;
    [SerializeField] GameObject playerImage;
    // Start is called before the first frame update
    void Start()
    {
        icon.enabled = false;
        animator = GetComponent<Animator>();
        mode = MODE.NULL;
        myScripts = this.GetComponent<StageSelectController>();
        myScripts.enabled = true;
        //si = Siabritukeru.GetComponent<SibariTukeru>();
        //si.enabled = false;
        Siabritukeru.SetActive(false);
        myPos = this.GetComponent<RectTransform>();
        for(int i = 0; i < Pos.Length; i++) {
            Pos[i] = Pos[i].GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("NOT"+not);
        StageIconMove();
        StageSelect();
        if(not)
        {
            ModeUi.SetActive(false);
            not = false;
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
            playerImage.SetActive(false);
            parent.SetBool("fade", true);
        }
    }

    void StageSelect() {
        if(myPos.localPosition == Pos[0].localPosition) {
            title.SelectSetumei(0);
            if(Gamepad.current.bButton.isPressed) {
                mode = MODE.STORY;
                parent.SetBool("fade",true);
                normal = true;
            }
            
        }
        if(myPos.localPosition == Pos[1].localPosition) {
            title.SelectSetumei(1);
            if(Gamepad.current.bButton.isPressed) {
                mode = MODE.CHALLENGE;
                parent.SetBool("fade", true);
                Siabritukeru.SetActive(true);
                StartCoroutine(SibariActive());
            }
        }
    }

    IEnumerator SibariActive() {
        yield return new WaitForSeconds(0.5f);
        //si.enabled = true;
      
        //myScripts.enabled = false;
    }
}

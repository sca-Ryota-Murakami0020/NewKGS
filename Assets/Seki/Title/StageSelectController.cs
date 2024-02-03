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

    public static MODE mode;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
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
        StageIconMove();
        StageSelect();
     
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
    }

    void StageSelect() {
        if(myPos.localPosition == Pos[0].localPosition) {
            title.SelectSetumei(0);
            if(Gamepad.current.bButton.isPressed) {
                mode = MODE.STORY;
                normal = true;
            }
            
        }
        if(myPos.localPosition == Pos[1].localPosition) {
            title.SelectSetumei(1);
            if(Gamepad.current.bButton.isPressed) {
                mode = MODE.CHALLENGE;
                Siabritukeru.SetActive(true);
                StartCoroutine(SibariActive());
            }
        }
    }

    IEnumerator SibariActive() {
        yield return new WaitForSeconds(0.5f);
        //si.enabled = true;
        myScripts.enabled = false;
    }
}

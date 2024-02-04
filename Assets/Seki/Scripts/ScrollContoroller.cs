using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ScrollContoroller : MonoBehaviour
{
    [SerializeField]
    private float textScrollSpeed;
    //　テキストの制限位置
    [SerializeField]
    private float limitPosition = 3900f;

    //　エンドロールが終了したかどうか
    private bool isStopEndRoll;

    [SerializeField] RawImage fadeImage;
    float alfa;
   
    bool scroll = false;
    bool wait = true;
    [SerializeField] Animator skipUI;
    [SerializeField] RectTransform[] selectPos;
    [SerializeField] RectTransform skipIcon;

    [SerializeField] GameObject[] backImage;

    [SerializeField] Animator finishAnim;
    [SerializeField] Animator finishPanel;

    bool fadeOut = false;
    float fadeSpeed = 0.02f;
   
    [SerializeField] Animator[] Moji;
    [SerializeField] GameObject[] MojiText;
    [SerializeField] Animator skipIconAnim;
    [SerializeField] GameObject[] underUI;
    // Start is called before the first frame update
    void Start()
    {
        skipIconAnim.enabled = false;
        for (int i = 0; i < underUI.Length; i++)
        {
            underUI[i].SetActive(false);
        }
        finishAnim.enabled =false;
        finishPanel.enabled = false;
        for(int i = 0; i < backImage.Length; i++) {
            backImage[i].SetActive(false);
        }
        alfa = fadeImage.color.a;
        for(int i = 0; i < Moji.Length; i++) {
            Moji[i].enabled = false;
            
        }
        for(int m = 0; m < MojiText.Length;m++) {
            MojiText[m].SetActive(false);
        }
        MojiActive(SousaUIContorller.stageClear);//
        Invoke("WaitFlag",3.5f);
    }
    void WaitFlag() {
        wait = false;
    }

    void MojiActive(int s) {
        switch(s) {
            case 0:
                Moji[0].enabled = true;
                MojiText[0].SetActive(true);
                Moji[1].enabled = false;
                MojiText[1].SetActive(false);
                Moji[2].enabled = false;
                MojiText[2].SetActive(false);
                MojiText[3].SetActive(false);
                backImage[0].SetActive(true);
                break;
            case 1:
                Moji[1].enabled = true;
                MojiText[1].SetActive(true);
                Moji[0].enabled = false;
                MojiText[0].SetActive(false);
                Moji[2].enabled = false;
                MojiText[2].SetActive(false);
                MojiText[3].SetActive(false);
                backImage[1].SetActive(true);
                break;
            case 2:
                Moji[2].enabled = true;
                MojiText[2].SetActive(true);
                Moji[0].enabled = false;
                MojiText[0].SetActive(false);
                Moji[1].enabled = false;
                MojiText[1].SetActive(false);
                MojiText[3].SetActive(false);
                backImage[2].SetActive(true);
                break;
            case 3:
                MojiText[3].SetActive(true);
                Moji[2].enabled = false;
                MojiText[2].SetActive(false);
                Moji[1].enabled = false;
                MojiText[1].SetActive(false);
                Moji[0].enabled = false;
                MojiText[0].SetActive(false);
                backImage[3].SetActive(true);
                break;
        }
    }

    bool start = false;
    // Update is called once per frame
    void Update()
    {
        if(!scroll && !wait) { 
            if(transform.position.y <= limitPosition) {
                 transform.position = new Vector3(transform.position.x, transform.position.y+textScrollSpeed * Time.deltaTime, transform.position.z + textScrollSpeed * Time.deltaTime);
            } else {
                fadeOut = true;
                if(SousaUIContorller.stageClear != 3) {//SousaUIContorller.stageClear
                    start = true;
                }
            }

            if(Gamepad.current.startButton.isPressed) {
                skipUI.SetBool("skip",true);
                skipIconAnim.enabled = true;
                scroll = true;
            }
        } else if(scroll && !fadeOut) {
            StartCoroutine(WaitSousa());
            
        }

        if(SousaUIContorller.stageClear == 3 && fadeOut) {//SousaUIContorller.stageClear
            finishAnim.enabled = true;
            finishPanel.enabled = true;
            StartCoroutine(WaitStart());
        }
      
    }

    IEnumerator WaitStart() {
        yield return new WaitForSeconds(5.0f);
        start  = true;
    }

    private void FixedUpdate() {
        if(start && fadeOut) {
            FadeOut();
        }
    }

    void FadeOut() {
        alfa += fadeSpeed;
        SetAlpha();
        if(alfa >= 1f) {
            if(StageSelectController.mode == StageSelectController.MODE.STORY) {
                StageCount(SousaUIContorller.stageClear);
            }
           
            SceneManager.LoadScene("LoadScene");
        }
    }

    void StageCount(int y) {
        switch(y) {
            case 0:
                TitleManager.sceneName = "Murakami";
                break;
            case 1:
                TitleManager.sceneName = "未来ステージ";//未来ステージ
                break;
            case 2:
                TitleManager.sceneName = "過去ステージ";//白亜紀ステージ
                break;
            case 3:
                TitleManager.sceneName = "Masaki";//タイトルステージ
                break;
        }
    }

    void SetAlpha() {
        fadeImage.color = new Color(0, 0, 0, alfa);
    }

    void underObj(int c)
    {
        for(int i = 0; i < underUI.Length; i++)
        {
            if(i == c)
            {
                underUI[c].SetActive(true);
            }
            else
            {
                underUI[i].SetActive(false);
            }
        }
    }

    void SkipIconMove() {
        if(Gamepad.current.leftStick.left.wasPressedThisFrame) {
            skipIcon.localPosition = selectPos[0].localPosition;
          
        }

        if(Gamepad.current.leftStick.right.wasPressedThisFrame) {
            skipIcon.localPosition = selectPos[1].localPosition;
            
        }

       
        if(skipIcon.localPosition == selectPos[0].localPosition) {
            underObj(0);
            skipIconAnim.SetBool("skip",false);
            if (Gamepad.current.bButton.wasPressedThisFrame) {
                Debug.Log("はい");
                start = true;
                fadeOut = true;
                }
            }

            if(skipIcon.localPosition == selectPos[1].localPosition) {
            underObj(1);
            skipIconAnim.SetBool("skip", true);
            if (Gamepad.current.bButton.wasPressedThisFrame) {
                    Debug.Log("いいえ");
                    StartCoroutine(ResetIcon());
                    skipUI.SetBool("skip", false);
                    scroll = false;
                skipIconAnim.enabled = false;
            }
            }
        
        }

        IEnumerator WaitSousa() {
        yield return new WaitForSeconds(1.0f);
        SkipIconMove();
    }

    IEnumerator ResetIcon() {
        yield return null;
        skipIcon.localPosition = selectPos[0].localPosition;
    }
}

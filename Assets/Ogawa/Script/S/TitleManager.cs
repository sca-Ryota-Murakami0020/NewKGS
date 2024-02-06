using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject IconObject;
    IconController Icon;
    [SerializeField] GameObject SibaritukeruObject;
    SibariTukeru sibaritukeru;

    [SerializeField] Image FadeObj;
    float alfa;

    float fadeSpeed = 0.02f;

    [SerializeField] Hontoni hontoi;

    [SerializeField] StageSelectController stageSelect;

    [Header("モードセレクトUI"), SerializeField]
    GameObject modeSelect;
    [Header("縛りの否かUI"), SerializeField]
    GameObject tukeru;
    [Header("縛り選択UI"), SerializeField]
    GameObject sibariSelect;
    [Header("縛り確認UI"), SerializeField]
    GameObject sibariKakunin;
    [Header("TitleUI"), SerializeField]
    Animator title;

    public static string sceneName;

    [SerializeField] GameObject playerImage;
    [SerializeField] GameObject[] titleSetumeiText;
    //[SerializeField] GameObject[] playerComent;
 //   [SerializeField] GameObject playerModel;
   // Animator anim;

    bool fade = false;
    public bool TITLEFADE {
        set {
            this.fade = value;
        }
        get {
            return this.fade;
        }
    }
    [SerializeField] Animator titleIcon;
    //テスト
    // Start is called before the first frame update
    void Start()
    {
        title.enabled = false;
        titleIcon.enabled = false;
        //Cursor.visible = false;
        SousaUIContorller.stageClear = 0;
        //anim = playerModel.GetComponent<Animator>();
        /*playerModel.SetActive(false);
        for(int t = 0; t < titleSetumeiText.Length; t++) {
            titleSetumeiText[t].SetActive(false);
            playerComent[t].SetActive(false);
        }
        */
        playerImage.SetActive(false);
        stageSelect = stageSelect.GetComponent<StageSelectController>();

        Icon = IconObject.GetComponent<IconController>();
        Icon.enabled = false;
        sibaritukeru = SibaritukeruObject.GetComponent<SibariTukeru>();
        FadeObj = FadeObj.GetComponent<Image>();
        alfa = FadeObj.color.a;
        sibaritukeru = SibaritukeruObject.GetComponent<SibariTukeru>();
        Invoke("InconActive", 0.5f);
    }

    void InconActive() {
        Icon.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Icon.STAGE) {
            Icon.enabled = true;
            playerImage.SetActive(true);
            titleIcon.enabled = true;
            //playerModel.SetActive(true);
            //IconObject.SetActive(false);
            Icon.STAGE = false;
        }
        if(sibaritukeru.SIBARIFLAG) {
            SibaritukeruObject.SetActive(false);
            sibaritukeru.SIBARIFLAG = false;
        }

        if(sibaritukeru.NO) {
            SelectSetumei(4);
            //FadeOut();
            sibaritukeru.enabled = false;
            //fade = true;
        }

        if(hontoi.YES) {
            SelectSetumei(4);
            hontoi.enabled = false;

            //fade = true;
        }

        if(stageSelect.NORMAL) {
            SelectSetumei(4);
            stageSelect.enabled = false;
            title.enabled = true;
            //fade = true;

            //FadeOut();
        }

        if(Gamepad.current.yButton.isPressed) {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
    }

    private void FixedUpdate() {
        if(fade) {
            StartCoroutine(WaitFade());
        }

    }
 

    void FadeOut() {
        alfa += fadeSpeed;
        SetAlpha();
        if(alfa >= 1.0f) {
            sibaritukeru.NO = false;
            hontoi.YES = false;
            stageSelect.NORMAL = false;
            fade = false;
            if(StageSelectController.mode == StageSelectController.MODE.STORY) {
                sceneName = "MojiHyouji";
            } else if(StageSelectController.mode == StageSelectController.MODE.CHALLENGE) {
                sceneName = "Murakami";
            }
            SceneManager.LoadScene("LoadScene");
        }
    }

    public void SelectSetumei(int count) {
        switch(count) {
            case 0:
                //anim.SetBool("ganba", false);
                //anim.SetBool("hazu", true);
                break;
            case 1:
                //anim.SetBool("hazu", false);
                //anim.SetBool("ganba", true);
                break;
            case 2:
                //anim.SetBool("ganba", false);
                //anim.SetBool("quetion", true);
                break;
            case 3:
                //anim.SetBool("think", true);
                //anim.SetBool("quetion", false);
                break;
            case 4:
                //anim.SetBool("think", false);
                //anim.SetBool("ready", true);
                break;
            case 5:
                //anim.SetBool("ready", false);
                //anim.SetBool("quetion", false);
                //anim.SetBool("ok", true);
                //anim.SetBool("hazu", false);
                break;
        }
        for(int u = 0; u < titleSetumeiText.Length; u++) {
            if(u == count) {
                titleSetumeiText[count].SetActive(true);
                //playerComent[count].SetActive(true);
            } else {
                titleSetumeiText[u].SetActive(false);
                //playerComent[u].SetActive(false);
            }
        }
    }

    void SetAlpha() {
        FadeObj.color = new Color(0, 0, 0, alfa);
    }

    IEnumerator WaitFade() {
        yield return new WaitForSeconds(2.0f);
        FadeOut();
    }
}

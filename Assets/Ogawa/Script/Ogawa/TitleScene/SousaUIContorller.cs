using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SousaUIContorller : MonoBehaviour
{
    //ページの変数
    int Pagecount = 0;
    //ステージ関係画面
    [SerializeField] GameObject[] stageImage;
    //表示するゲーム画面
    [SerializeField] GameObject[] gameImage;
    //ページ数が分かるUI
    [SerializeField] Image[] nextImage;
    //ギミック関係の画面
    [SerializeField] GameObject[] gimikkuImage;

    [SerializeField] GameObject _loadingUI;

    [SerializeField] GameObject sliderBar;
    Slider _slider;

    [SerializeField] private Text _text;
    [SerializeField] GameObject okText;

    float tipsTime;

    bool tips = false;

 
    AsyncOperation async;

   // [SerializeField] GameObject playerObj;
   // Vector3 pos;
   // Animator playeranim;

    bool ok = false;
    bool move = false;

    public static int stageClear;

    
    // Start is called before the first frame update

    void Start()
    {
       
        //pos = playerObj.transform.position;
        //playeranim = playerObj.GetComponent<Animator>();
        okText.SetActive(false);

        _slider = sliderBar.GetComponent<Slider>();
        //表示するゲーム画面の初期化
        for(int i = 0; i < gameImage.Length; i++) {
            gameImage[i].SetActive(false);
        }

        for(int u = 0; u < stageImage.Length; u++) {
            stageImage[u].SetActive(false);
        }
        for(int u = 0; u < gimikkuImage.Length; u++) {
            gimikkuImage[u].SetActive(false);
        }

        LoadNextScene();
    }

  

    public void LoadNextScene() {
        _loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene() {
        yield return null;
        async = SceneManager.LoadSceneAsync(TitleManager.sceneName);
        async.allowSceneActivation = false;
        while(!async.isDone) {
            _slider.value = async.progress * 100;

            _text.text = (async.progress * 100).ToString() + "%";

            //Pagecount = (int)async.progress % 5;
            if(async.progress >= 0.9f) {
                _slider.value = 100.0f;
                _text.text = "100%";
                StartCoroutine(Waitok());
                if(Gamepad.current.bButton.wasPressedThisFrame) {//ok && 
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    IEnumerator Waitok() {
        yield return new WaitForSeconds(1.0f);
        sliderBar.SetActive(false);
        _text.enabled = false;
        okText.SetActive(true);
        //playerObj.SetActive(false);
        ok = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_slider.value != 0 && _slider.value % 10 == 0 && _slider.value < 100.0f) {
            move = true;
        }

        if(move) {
            //pos.x += 1.8f;
           // playerObj.transform.position = pos;
            move = false;
        }

        if(!tips ) {//&& _slider.value < 100.0f
            tipsTime += Time.deltaTime;
            if((int)tipsTime != 0 && (int)tipsTime % 5 == 0) {
                tips = true;
            }
        }

        if(_slider.value >= 100.0f) {
            //playeranim.SetBool("Fall", true);
        }

        if(tips) {
            //_slider.value += 10f;//デバックようにスライダーの値をいじっただけ
            tips = false;
            if(Pagecount == 4) {
                Pagecount = 0;
            } else {
                Pagecount++;
            }
            tipsTime = 0f;

        }
        ImageNext(Pagecount, stageClear);
    }

    /// <summary>
    /// //Imageの色や非表示、表示などを行う関数
    /// </summary>
    /// <param name="count">ページ数</param>
    void ImageNext(int count, int random) {
        for(int i = 0; i < nextImage.Length; i++) {
            if(i == count) {//ページ数が同じなら
                if(random == 0) {
                    gameImage[count].SetActive(true);
                } else if(random == 1){
                    stageImage[count].SetActive(true);
                }
                else if(random == 2) {
                    gimikkuImage[count].SetActive(true);
                }

                nextImage[count].color = new Color(255, 0, 0);
            } else {
                if(random == 0) {
                    gameImage[i].SetActive(false);
                } else if(random == 1) {
                    stageImage[i].SetActive(false);
                } else if(random == 2) {
                    gimikkuImage[count].SetActive(false);
                }
                nextImage[i].color = new Color(255, 255, 255);
            }
        }
    }
}

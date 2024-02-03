using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideUIControl : MonoBehaviour
{
    public int state = 0;
    public bool loop = false;

    public bool GameClear = false;

    [Header("Text")]
    public Vector3 outPos01;
    public Vector3 inPos;
    public Vector3 outPos02;

    // シングルトンインスタンス
    private static SlideUIControl instance;

    // インスタンスにアクセスするためのプロパティ
    public static SlideUIControl Instance
    {
        get
        {
            // インスタンスがまだ作成されていない場合は新しく作成
            if (instance == null)
            {
                instance = FindObjectOfType<SlideUIControl>();

                // シーン内に存在しない場合はエラーログを表示
                if (instance == null)
                {
                    Debug.LogError("SlideUIControl インスタンスが見つかりませんでした。");
                }
            }

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //動作確認用
        if (Input.GetKeyDown("space"))
        {
            
        }
        */
        GameClear = true;


        if (GameClear)
        {
            if (state == 1)
            {
                if (transform.localPosition.x > inPos.x - 1.0f &&
                    transform.localPosition.y > inPos.y - 1.0f &&
                    transform.localPosition.z > inPos.z - 1.0f)
                {
                    transform.localPosition = inPos;
                }

                else
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, 4.0f * Time.unscaledDeltaTime);
                }
            }
        }

    }

    public bool IsGamePaused
    {
        get { return GameClear; }
    }

    // セッター
    public void SetGamePaused(bool value)
    {
        GameClear = value;
    }
}

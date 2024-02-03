using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTitle : MonoBehaviour
{
    [SerializeField] TitleManager titleManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAnimationEnd() {
        //アニメーション終了時の処理
        titleManager.TITLEFADE = true;
    }
}

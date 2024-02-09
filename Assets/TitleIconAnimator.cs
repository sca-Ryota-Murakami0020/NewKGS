using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleIconAnimator : MonoBehaviour
{
    [SerializeField] IconController icon;
    [SerializeField] StageSelectController stage;
    [SerializeField] Animator printObj;
    [SerializeField] Animator mode;
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
        if(!icon.STORY && !stage.MODEFLAG) {
            icon.TITLEFADE = true;
            //stage.enabled = true;
            //stage.MODEFLAG = false;
        } 
        
        else {
            printObj.enabled = true;
            printObj.SetBool("rank",false);
        }
        //Debug.Log("終わった");
    }
}

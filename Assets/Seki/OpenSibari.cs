using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSibari : MonoBehaviour
{
    [SerializeField] DemoIcon demoIcon;
    [SerializeField] TitleManager titleManager;
    [SerializeField] GameObject Siabritukeru;
    [SerializeField] GameObject stageMode;
    [SerializeField] StageSelectController stage;
    [SerializeField] Animator title;
    [SerializeField] GameObject playerImage;
   
    public void OnStartAnim() {
        titleManager.SelectSetumei(2);
        demoIcon.enabled = true;
        demoIcon.SOUSA = true;
    }

    public void OnFinishAnim() {
        demoIcon.SOUSA = false;
        Siabritukeru.SetActive(false);
        if(!demoIcon.GO) {
            stageMode.SetActive(true);
            
            stage.enabled = true;
        }
        
        demoIcon.enabled = false;
        if(demoIcon.GO) {
            title.enabled = true;
            playerImage.SetActive(false);
            //titleManager.TITLEFADE = true;
            //demoIcon.GO = false;
        }
    }
}

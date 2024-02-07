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
            titleManager.TITLEFADE = true;
            demoIcon.GO = false;
        }
    }
}

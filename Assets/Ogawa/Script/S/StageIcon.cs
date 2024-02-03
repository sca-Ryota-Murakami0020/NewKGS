using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageIcon : MonoBehaviour
{
    RectTransform mypos;
    [SerializeField] RectTransform[] stagepos;
    // Start is called before the first frame update
    void Start()
    {
        mypos = this.GetComponent<RectTransform>();
        for(int i = 0; i < stagepos.Length; i++) {
            stagepos[i] = stagepos[i].GetComponent<RectTransform>();
        }
        mypos.localPosition = stagepos[0].localPosition;
        Invoke("YesFlag",1.0f);
    }
    int count = 0;
    public bool YESFLAG {
        set {
            this.yesFlag = value;
        }
        get {
            return this.yesFlag;
        }
    }
    bool yesFlag = false;

    // Update is called once per frame
    void Update()
    {
        if(yesFlag) { 
        if(count > 0) {
            if(Input.GetKeyDown(KeyCode.UpArrow)) {
                count--;
                mypos.localPosition = stagepos[count].localPosition;
            }
        }

        if(count < 2) {
            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                count++;
                mypos.localPosition = stagepos[count].localPosition;
            }
        }
    }
    }

    void YesFlag() {
        yesFlag = true;
    }
}

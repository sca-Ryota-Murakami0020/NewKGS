using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconCotroller : MonoBehaviour
{
    RectTransform my;
    [SerializeField] GameObject stage;
    RectTransform st;
    [SerializeField] GameObject sousa;
    RectTransform so;

    bool stageFlag = false;
    public bool STAGE {
        set {
            this.stageFlag = value;
        }
        get {
            return this.stageFlag;
        }
    }
    bool sousaFlag = false;
    public bool SOUSA {
        set {
            this.sousaFlag = value;
        }
        get {
            return this.stageFlag;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        my = this.GetComponent<RectTransform>();
        st = stage.GetComponent<RectTransform>();
        so = sousa.GetComponent<RectTransform>();
        my.localPosition = st.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(stageFlag == false && sousaFlag == false) {
            IconPos();
        }

        if(my.localPosition == st.localPosition) {
            if(Input.GetKeyDown(KeyCode.P)) {
                stageFlag = true;
            }
        } else if(my.localPosition == so.localPosition) {
            if(Input.GetKeyDown(KeyCode.P)) {
                sousaFlag = true;
            }
        }
    }

    /// <summary>
    /// É^ÉCÉgÉãÇÃIconÇìÆÇ©Ç∑ä÷êî
    /// </summary>
    void IconPos() {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            my.localPosition = st.localPosition;

        } else if(Input.GetKeyDown(KeyCode.DownArrow)) {
            //Debug.Log("d");
            my.localPosition = so.localPosition;

        }
    }
}

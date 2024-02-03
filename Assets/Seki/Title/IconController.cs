using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IconController : MonoBehaviour
{
    RectTransform my;
    [SerializeField] RectTransform[] Title;
    bool stageFlag = false;
    public bool STAGE {
        set {
            this.stageFlag = value;
        }
        get {
            return this.stageFlag;
        }
    }
  
    [SerializeField] GameObject Siabritukeru;

    // Start is called before the first frame update
    void Start()
    {
        my = this.GetComponent<RectTransform>();

        Siabritukeru.SetActive(false);
        my.localPosition = Title[0].localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stageFlag) {
            IconPos();
        }
        


        if(my.localPosition == Title[0].localPosition) {
            if(Gamepad.current.bButton.wasReleasedThisFrame) {
                stageFlag = true;
                Siabritukeru.SetActive(true);
            }
        }

        if(my.localPosition == Title[1].localPosition) {
            if(Gamepad.current.bButton.wasReleasedThisFrame) {
                //stageFlag = true;
                //Siabritukeru.SetActive(true);
            }
        }

    }


    /// <summary>
    /// É^ÉCÉgÉãÇÃIconÇìÆÇ©Ç∑ä÷êî
    /// </summary>
    void IconPos() {
        if(Gamepad.current.leftStick.up.wasReleasedThisFrame) {
            my.localPosition = Title[0].localPosition;

        }
        if(Gamepad.current.leftStick.down.wasReleasedThisFrame) {
            my.localPosition = Title[1].localPosition;

        }
    }
}

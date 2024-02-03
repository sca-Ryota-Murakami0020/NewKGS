using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Hontoni : MonoBehaviour
{
    [SerializeField] RectTransform[] Pos;
    RectTransform myPos;
    [SerializeField] SibariUIController sibari;
    [SerializeField] GameObject SibariUI;
    [SerializeField] GameObject hontoUI;
    [SerializeField] RawImage[] Icon;
    [SerializeField] GameObject[] RevelText;
    [SerializeField] RawImage[] SibariIcon;
    
    bool no = false;
    public bool NO {
        set {
            this.no = value;
        }
        get {
            return this.no;
        }
    }

    bool yes = false;
    public bool YES {
        set {
            this.yes = value;
        }
        get {
            return this.yes;
        }
    }

    [SerializeField] TitleManager titleManager;

    // Start is called before the first frame update
    void Start()
    {
        titleManager.SelectSetumei(3);
        for(int i = 0; i < RevelText.Length; i++) {
            RevelText[i].SetActive(false);
        }
        myPos = this.GetComponent<RectTransform>();
        sibari = sibari.GetComponent<SibariUIController>();
        for(int i = 0; i < Icon.Length; i++) {
            Icon[i] = Icon[i].GetComponent<RawImage>();
            SibariIcon[i] = SibariIcon[i].GetComponent<RawImage>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        


        for(int i = 0; i < Icon.Length; i++) {
             if(sibari.SENTAKU[i] == 1) {
                Icon[i].color = new Color32(255,255,255,255);
                SibariIcon[i].color = new Color32(255, 255, 255, 255);
            } else {
                Icon[i].color = new Color32(181, 181, 181, 255);
                SibariIcon[i].color = new Color32(181, 181, 181, 255);
            }
        }
        for(int i = 0; i < RevelText.Length; i++) {
            if(sibari.REVEL == i) {
                RevelText[i].SetActive(true);
            } else {
                RevelText[i].SetActive(false);
            }
        }

        if(Gamepad.current.leftStick.right.wasPressedThisFrame) {
            myPos.localPosition = Pos[1].localPosition;
        }
        if(Gamepad.current.leftStick.left.wasPressedThisFrame) {
            myPos.localPosition = Pos[0].localPosition;
        }

        if(myPos.localPosition == Pos[0].localPosition) {
            if(Gamepad.current.bButton.wasPressedThisFrame) {
                Debug.Log("‚Í‚¢");
                yes = true;
            }

          
        }

        if(myPos.localPosition == Pos[1].localPosition) {
            if(Gamepad.current.bButton.isPressed) {
                Debug.Log("‚¢‚¢‚¦");
                no=true;
                titleManager.SelectSetumei(4);
                StartCoroutine(WaitSousa());
                SibariUI.SetActive(true);
                
            }
        }
    }


    IEnumerator WaitSousa() {
        yield return new WaitForSeconds(1.0f);
        myPos.localPosition = Pos[0].localPosition;
        sibari.enabled = true;
        sibari.NEXT = false;
        hontoUI.SetActive(false);
    }
}

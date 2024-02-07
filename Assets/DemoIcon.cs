using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DemoIcon : MonoBehaviour
{
    int leftright = 0;
    int levelCount = 0;
    int levelCopy = 0;
    int[] bCount;
    int[] sibariCount;
    public int[] SENTAKUCOUNT {
        set {
            this.sibariCount = value;
        }
        get {
            return this.sibariCount;
        }
    }
    [SerializeField] RectTransform myPos;
    [SerializeField] RectTransform[] Pos;
    [SerializeField] GameObject[] oneSibariIcon;
    [SerializeField] GameObject[] twoSibariIcon;
    [SerializeField] GameObject[] treeSibariIcon;
    [SerializeField] Text levelText;
    [SerializeField] RawImage[] backImage;
    [SerializeField] RectTransform deciedPos;
    [SerializeField] Sprite[] normalIcon;
    [SerializeField] Sprite[] hardIcon;
    [SerializeField] Sprite[] exIcon;
    [SerializeField] Image iconImage;
    [SerializeField] Animator deceid;
    [SerializeField] GameObject[] emptyText;
    [SerializeField] GameObject[] oneText;
    [SerializeField] GameObject[] twoText;
    [SerializeField] GameObject[] treeText;
    private PlayerManager playerManager;
    [SerializeField] Animator sibariUnder;
    bool go = false;
    public bool GO {
        set {
            this.go = value;
        }
        get {
            return this.go;
        }
    }
    bool sousa = false;
    public bool SOUSA {
        set {
            this.sousa = value;
        }
        get {
            return this.sousa;
        }
    }

    [SerializeField] Animator parent;
    [SerializeField] TitleManager titleManager;
    // Start is called before the first frame update
    void Start()
    {
        
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        iconImage.sprite = normalIcon[0];
        for(int i = 0; i < emptyText.Length; i++) {
            emptyText[i].SetActive(false);
            oneText[i].SetActive(false);
            twoText[i].SetActive(false);
            treeText[i].SetActive(false);
        }
        sibariCount = new int[6];
        bCount = new int[6];
        for(int i = 0; i < backImage.Length; i++) {
            backImage[i].color = new Color32(0,0,0,89);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("縛りカウント"+ leftright);
        levelCopy =levelCount;
        levelText.text = "(レベル"+ (levelCopy + 1)+")";
        if(sousa) { 
        StickMove(leftright);
        LeftRightStick();
        levelIcon();
        if(myPos.localPosition == Pos[0].localPosition) {
        
            PosButton(0);
            ChangeIcon(levelCount,0);
        }
        else if(myPos.localPosition == Pos[1].localPosition) {
            
            PosButton(1);
            ChangeIcon(levelCount, 1);
        }
        else if(myPos.localPosition == Pos[2].localPosition) {
            
            PosButton(2);
            ChangeIcon(levelCount, 2);
        }
        else if(myPos.localPosition == Pos[3].localPosition) {
            
            PosButton(3);
            ChangeIcon(levelCount, 3);
        }
        else if(myPos.localPosition == Pos[4].localPosition) {
            
            PosButton(4);
            ChangeIcon(levelCount, 4);
        }
        else if(myPos.localPosition == Pos[5].localPosition) {
            
            PosButton(5);
            ChangeIcon(levelCount, 5);
            }
            if(myPos.localPosition == deciedPos.localPosition) {

                deceid.SetBool("decid", true);
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                    Debug.Log("決定");
                    parent.SetBool("sibariFlag",true);
                    sousa = false;
                    go = true;
                }
            } else {

                deceid.SetBool("decid", false);
            }
        }

        if(Gamepad.current.aButton.wasPressedThisFrame) {
            parent.SetBool("sibariFlag", true);
            this.enabled = false;
        }
    }

        void ChangeIcon(int l,int c) {
        for(int i = 0; i < emptyText.Length; i++) {
            if(i == c) {
                emptyText[c].SetActive(true);
            }
            else {
                emptyText[i].SetActive(false);
            }
        }
        if(l == 0) {
            iconImage.sprite = normalIcon[c];
            oneText[c].SetActive(true);
            twoText[c].SetActive(false);
            treeText[c].SetActive(false);
        }
        else if(l == 1) {
            iconImage.sprite = hardIcon[c];
            twoText[c].SetActive(true);
            oneText[c].SetActive(false);
            treeText[c].SetActive(false);
        }
        else if(l == 2) {
            iconImage.sprite = exIcon[c];
            treeText[c].SetActive(true);
            oneText[c].SetActive(false);
            twoText[c].SetActive(false);
        }
    }

    void PosButton(int c) {
        if(Gamepad.current.bButton.wasPressedThisFrame) {
            bCount[c]++;
            if(bCount[c] == 3) {
                bCount[c] = 1;
            }
            ChangeBack(c, bCount);
        }
    }

    void ChangeBack(int c,int[] j) {
        if(j[c] == 1) {
            sibariCount[c]++;
            
            backImage[c].color = new Color32(255, 255, 0, 130);
            
        }
        else if(j[c] == 2) {
            if(sibariCount[c] >= -1) {
                sibariCount[c]--;
            }
            
            
            backImage[c].color = new Color32(0, 0, 0, 89);
        }
    }

    void levelIcon() {
        if(Gamepad.current.leftShoulder.wasPressedThisFrame) {
            levelCount++;
            if(levelCount == 3) {
                levelCount = 0;
            }
            
        }

        switch(levelCount) {
            case 0:
                IconActive(levelCount);//, 
                break;
            case 1:
                IconActive(levelCount);//levelCount, 
                break;
            case 2:
                IconActive(levelCount);
                break;
        }
    }

    void IconActive(int c) {//int c,
        if(c == 0) {
            for(int i = 0; i < 5; i++) {
                oneSibariIcon[i].SetActive(true);
                twoSibariIcon[i].SetActive(false);
                treeSibariIcon[i].SetActive(false);
            }
        }
        else if(c == 1) {
            for(int i = 0; i < 5; i++) {
                oneSibariIcon[i].SetActive(false);
                twoSibariIcon[i].SetActive(true);
                treeSibariIcon[i].SetActive(false);
            }
        }
        else if(c == 2) {
            for(int i = 0; i < 5; i++) {
                oneSibariIcon[i].SetActive(false);
                twoSibariIcon[i].SetActive(false);
                treeSibariIcon[i].SetActive(true);
            }
        }
    }

    void StickMove(int c) {
        switch(c) {
            case 0:
                OneStick();
                break;
            case 1:
                TwoStick();
                break;
            case 2:
                TreeStick();
                break;
        }
    }

    void LeftRightStick() {
        if(Gamepad.current.leftStick.left.wasPressedThisFrame && leftright > 0) {
            sibariUnder.SetTrigger("sibari");
            leftright--;
            leftStck(leftright);
        }
        if(Gamepad.current.leftStick.right.wasPressedThisFrame && leftright < 2) {
            sibariUnder.SetTrigger("sibari");
            leftright++;
            rightStck(leftright);
        }
    }

    void rightStck(int c) {
        if(c == 1) {
            if(myPos.localPosition == Pos[0].localPosition) {
                myPos.localPosition = Pos[2].localPosition;
            }
            else if(myPos.localPosition == Pos[1].localPosition) {
                myPos.localPosition = Pos[3].localPosition;
            }
        }
        else if(c == 2) {
            if(myPos.localPosition == Pos[2].localPosition) {
                myPos.localPosition = Pos[4].localPosition;
            }
            else if(myPos.localPosition == Pos[3].localPosition) {
                myPos.localPosition = Pos[5].localPosition;
            }
        }
    }

    void leftStck(int c) {
        if(c == 1) {
            if(myPos.localPosition == Pos[4].localPosition) {
                myPos.localPosition = Pos[2].localPosition;
            }
            else if(myPos.localPosition == Pos[5].localPosition) {
                myPos.localPosition = Pos[3].localPosition;
            }

        } else if(c == 0) {
            if(myPos.localPosition == Pos[2].localPosition) {
                myPos.localPosition = Pos[0].localPosition;
            }
            else if(myPos.localPosition == Pos[3].localPosition) {
                myPos.localPosition = Pos[1].localPosition;
            }
        }
    }

    void OneStick() {
        if(Gamepad.current.leftStick.up.wasPressedThisFrame) {
            sibariUnder.SetTrigger("sibari");
            myPos.localPosition = Pos[0].localPosition;
        }
        if(Gamepad.current.leftStick.down.wasPressedThisFrame) {
            sibariUnder.SetTrigger("sibari");
            myPos.localPosition = Pos[1].localPosition;
        }
    }

    void TwoStick() {
        if(Gamepad.current.leftStick.up.wasPressedThisFrame) {
            sibariUnder.SetTrigger("sibari");
            myPos.localPosition = Pos[2].localPosition;
        }
        if(Gamepad.current.leftStick.down.wasPressedThisFrame) {
            sibariUnder.SetTrigger("sibari");
            myPos.localPosition = Pos[3].localPosition;
        }
    }

    int tree = 0;
    void TreeStick() {
        if(Gamepad.current.leftStick.up.wasPressedThisFrame) {
           if(tree > 0) {
              tree--;
           }
           if(tree == 0) {
                sibariUnder.SetTrigger("sibari");
                myPos.localPosition = Pos[4].localPosition;
           } else if(tree == 1) {
                sibariUnder.SetTrigger("sibari");
                myPos.localPosition = Pos[5].localPosition;
           }
        }
        if(Gamepad.current.leftStick.down.wasPressedThisFrame) {
            if(tree != 2) {
                tree++;
            }
            sibariUnder.SetTrigger("sibari");
            myPos.localPosition = Pos[5].localPosition;
        }
        if(Gamepad.current.leftStick.down.wasPressedThisFrame && tree == 2) {
            sibariUnder.SetTrigger("sibari");
            myPos.localPosition = deciedPos.localPosition;
        }
    }
}

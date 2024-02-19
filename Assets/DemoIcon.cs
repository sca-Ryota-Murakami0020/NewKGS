using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DemoIcon : MonoBehaviour
{
    int leftright = 0;
    //オールレベルカウント
    int levelCount = 0;
    
    int[] bCount;
    //個人スキルレベル
    int[] P_levelCopy;
    int[] P_level;
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

    [SerializeField] Image[] SibariIcon;
    [SerializeField] GameObject[] sound;
 
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SibariIcon.Length; i++) {
            SibariIcon[i].color = new Color32(147,147,147,255);
            SibariIcon[i].sprite = normalIcon[i];
        }
        P_level = new int[6];
        P_levelCopy = new int[6];
        for(int i = 0; i < P_level.Length; i++) {
            if(i == 5) {
                P_level[i] = 3;
            } else {
                P_level[i] = 1;
            }
        }
        bCount = new int[6];
        for(int b = 0 ;b < bCount.Length; b++) {
            bCount[b] = 2;
        }
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        iconImage.sprite = normalIcon[0];
        for(int i = 0; i < emptyText.Length; i++) {
            emptyText[i].SetActive(false);
            oneText[i].SetActive(false);
            twoText[i].SetActive(false);
            treeText[i].SetActive(false);
        }
      
        sibariCount = new int[6];
        
        for(int i = 0; i < backImage.Length; i++) {
            backImage[i].color = new Color32(0,0,0,89);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(sousa) { 
            StickMove(leftright);
            LeftRightStick();
      
            if(myPos.localPosition == Pos[0].localPosition) {
                soundMusic(0);
                PosButton(0);
                Pushlevel(0);
                ChangeIcon(P_level[0],0);
            }
            else if(myPos.localPosition == Pos[1].localPosition) {
                soundMusic(1);
                PosButton(1);
                Pushlevel(1);
                ChangeIcon(P_level[1], 1);
            }
            else if(myPos.localPosition == Pos[2].localPosition) {
                soundMusic(2);
                PosButton(2);
                Pushlevel(2);
                ChangeIcon(P_level[2], 2);
            }
            else if(myPos.localPosition == Pos[3].localPosition) {
                soundMusic(3);
                PosButton(3);
                Pushlevel(3);
                ChangeIcon(P_level[3], 3);
            }
            else if(myPos.localPosition == Pos[4].localPosition) {
                soundMusic(4);
                PosButton(4);
                Pushlevel(4);
                ChangeIcon(P_level[4], 4);
            }
            else if(myPos.localPosition == Pos[5].localPosition) {
                soundMusic(5);
                PosButton(5);
                Pushlevel(5);
                ChangeIcon(P_level[5], 5);
            }
            if(myPos.localPosition == deciedPos.localPosition) {
                
                deceid.SetBool("decid", true);
                if(Gamepad.current.bButton.wasPressedThisFrame) {
                   for(int i = 0; i < P_level.Length; i++) {
                        levelCount += P_levelCopy[i];
                   }
                    playerManager.GameLevel = levelCount;
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
            Start();
            this.enabled = false;
        }
    }

    void soundMusic(int c) {
        for(int i = 0; i < sound.Length; i++) {
            if(i == c) {
                sound[c].SetActive(true);
            } else {
                sound[i].SetActive(false);
            }
        }
    }

    void Pushlevel(int c) {
        if(bCount[c] == 1) {
            P_levelCopy[c] = P_level[c];
            levelIcon(c);
        }
        else if(bCount[c] == 2) {
            P_levelCopy[c] = 0;
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
        if(l == 1) {
            iconImage.sprite = normalIcon[c];
            oneText[c].SetActive(true);
            twoText[c].SetActive(false);
            treeText[c].SetActive(false);
        }
        else if(l == 2) {
            iconImage.sprite = hardIcon[c];
            twoText[c].SetActive(true);
            oneText[c].SetActive(false);
            treeText[c].SetActive(false);
        }
        else if(l == 3) {
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
            
            SibariIcon[c].color = new Color32(255,255,255,255);
            backImage[c].color = new Color32(255, 255, 0, 130);
            
        }
        else if(j[c] == 2) {
            if(sibariCount[c] >= -1) {
                sibariCount[c]--;
            }
           
            SibariIcon[c].color = new Color32(147, 147, 147, 255);
            backImage[c].color = new Color32(0, 0, 0, 89);
        }
    }

    void levelIcon(int c) {
        if(Gamepad.current.leftShoulder.wasPressedThisFrame) {
            if(c == 5) {
                P_level[5] = 3;
            }
            else {
                P_level[c]++;
            }
            
            if(c != 5 && P_level[c] == 4) {
                P_level[c] = 1;
            }
        }
        
        
        IconActive(P_level[c], c);

    }

    void IconActive(int c,int l) {//int c,
        if(c == 1) {
            for(int i = 0; i < 6; i++) {
                if(l == i) {
                    SibariIcon[l].sprite = normalIcon[l];
                }
                   //SibariIcon[i].sprite = normalIcon[i];
                
            }
        }
        else if(c == 2) {
            for(int i = 0; i < 6; i++) {
                if(l == i) {
                    SibariIcon[l].sprite = hardIcon[l];
                } 
                    //SibariIcon[i].sprite = hardIcon[i];
                
            }
        }
        else if(c == 3) {
            for(int i = 0; i < 6; i++) {
                if(i == l) {
                    SibariIcon[l].sprite = exIcon[l];
                } 
                    //SibariIcon[i].sprite = exIcon[i];
                
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

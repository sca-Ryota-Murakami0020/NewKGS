using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LetterPanel : MonoBehaviour
{
    [SerializeField] Letter letter;
    [SerializeField] TextAsset letterText;
    [SerializeField] Text nameText;

    List<Letter> letters;
    int maxNameLength = 8;
    int currentIndex = 0;
    int copyIndex = 0;
    NameScripts s_name;
    bool next = false;

    public bool NEXT {
        set {
            this.next = value;
        }
        get {
            return this.next;
        }
    }
    //[SerializeField] Text TextName;

    [SerializeField] MojiManager moji;
    [SerializeField] GameObject myName;
    // Start is called before the first frame update
    void Start()
    {
        //TextName = TextName.GetComponent<Text>();
        s_name = nameText.GetComponent<NameScripts>();
        //í«â¡Å@ListÇèâä˙âª
        letters = new List<Letter>();
        string[] letterLines = letterText.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach(string letterLine in letterLines) {
            string[] letterValues = letterLine.Split(new char[] { ',' });
            foreach(string l in letterValues) {
                //í«â¡
                Letter letterObj = Instantiate(letter, transform);
                letters.Add(letterObj);
                StartCoroutine(_setLetter(l, letterObj));
            }
        }
        ShowArrow(copyIndex);
    }

    //í«â¡
    IEnumerator _setLetter(string _l, Letter _letterObj) {
        yield return null;
        _letterObj.SetLetter(_l);
    }
   
    
    // Update is called once per frame
    void Update()
    {

        if(moji.WRITE) { 
        if(Gamepad.current.dpad.right.wasPressedThisFrame) {
           //if(copyIndex != 60 && copyIndex != 64) { 
            currentIndex++;
            if(Empty(currentIndex) == false && YesCurrent(currentIndex) == false) {
                copyIndex = currentIndex;
            } else {
              copyIndex+=2;
            }
                ShowArrow(copyIndex);
           //}
           
        }
        if(Gamepad.current.dpad.left.wasPressedThisFrame) {
            if(copyIndex > 0 && copyIndex != 88) {
                currentIndex--;
                if(Empty(currentIndex) == false && YesCurrent(currentIndex) == false) {
                    copyIndex = currentIndex;
               } else {
                 copyIndex -= 2;
             }
                ShowArrow(copyIndex);
           }
        }
        if(Gamepad.current.dpad.down.wasPressedThisFrame) {
           //if(copyIndex != 46 && copyIndex != 48 && copyIndex != 63 && copyIndex != 64) {
            currentIndex += 11;
            if(Empty(currentIndex) == false && YesCurrent(currentIndex) == false) {
               copyIndex = currentIndex;
            } else {
                copyIndex += 22;
           }
            //copyIndex = currentIndex;
            ShowArrow(copyIndex);
          // }
        }
        if(Gamepad.current.dpad.up.wasPressedThisFrame) {
            //if(copyIndex > 0 && copyIndex != 63 && copyIndex != 1 && copyIndex != 2 && copyIndex != 3 && copyIndex != 4) {
                currentIndex -= 11;
            if(Empty(currentIndex) == false && YesCurrent(currentIndex) == false) {
                copyIndex = currentIndex;
            } else {
                copyIndex -= 22;
            }
                ShowArrow(copyIndex);
              //  }

        }
        if(Gamepad.current.xButton.wasPressedThisFrame) {
            //TextName.text = "";
        }

        if(Gamepad.current.bButton.wasPressedThisFrame) {
            if(s_name.MYNAME.Length <= maxNameLength) {
                if(letters[copyIndex].SelectLetter() == "åàíË") {
                    next = true;
                    myName.SetActive(false);
                    moji.WRITE = false;
                }
            }
            if(s_name.MYNAME.Length < maxNameLength) {

                if(YesCurrent(currentIndex) == false) {
                if(letters[copyIndex].SelectLetter() == "ãÛîí") {//
                    s_name.MYNAME += "Å@";
                    
                }
                else if(letters[copyIndex].SelectLetter() != "åàíË") {
                    s_name.MYNAME += letters[copyIndex].SelectLetter();
                }
                nameText.text = s_name.MYNAME;
                }
            }

        }

        if(Gamepad.current.aButton.wasPressedThisFrame) {
            if(s_name.MYNAME.Length > 0) {
                s_name.MYNAME = s_name.MYNAME.Remove(s_name.MYNAME.Length - 1);
            }
            nameText.text = s_name.MYNAME;
        }
        }
    }

    bool Empty(int c) {
        bool y = false;
        switch(c) {
            case 5:
                y = true;
                break;
            case 16:
                y = true;
                break;
            case 27:
                y = true;
                break;
            case 38:
                y = true;
                break;
            case 49:
                y = true;
                break;
            case 60:
                y = true;
                break;
            case 71:
                y = true;
                break;
            case 82:
                y = true;
                break;
            default:
                y = false;
                break;
        }
        return y;
    }
    

    bool YesCurrent(int c) {
        bool yes = false;
        switch(c) {
            case 40:
                yes = true;
                break;
            case 42:
                yes = true;
                break;
            case 87:
                yes = true;
                break;
            case 89:
                yes = true;
                break;
            case 91:
                yes = true;
                break;
            case 93:
                yes = true;
                break;
            case 94:
                yes = true;
                break;
            case 95:
                yes = true;
                break;
            case 96:
                yes = true;
                break;
            default:
                yes = false;
                break;
        }
        return yes;
    }

    //í«â¡
    void ShowArrow(int c) {
        copyIndex = Mathf.Clamp(copyIndex, 0, letters.Count-1);
        for(int i = 0; i < letters.Count; i++) {
            if(i == copyIndex) {
                if(YesCurrent(c) == false) {
                    letters[i].Selected(true);
                }
               
            } else {
                letters[i].Selected(false);
            }
        }
    }
}

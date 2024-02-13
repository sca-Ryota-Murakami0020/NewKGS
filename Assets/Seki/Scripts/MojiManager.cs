using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MojiManager : MonoBehaviour
{
    [SerializeField] GameObject moji;
    [SerializeField] GameObject moji2;
    int count = 0;

    bool write = false;
    public bool WRITE {
        set {
            this.write = value;
        }
        get {
            return this.write;
        }
    }

    [SerializeField] GameObject playerRankIn;

    // Start is called before the first frame update
    void Start()
    {
     
        moji.SetActive(true);
        moji2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current.leftShoulder.wasPressedThisFrame) {
            count++;
            ActiveLangage(count);
            if(count == 2) {
                count = 0;
            }
        }
    }

    void ActiveLangage(int c) {
        if(c == 1) {
            moji.SetActive(false);
            moji2.SetActive(true);
        }
        else if(c == 2)
        {
            moji.SetActive(true);
            moji2.SetActive(false);
           
        }
    }

    public void OnAnimFinish() {
        playerRankIn.SetActive(true);
        write = true;
    }
}

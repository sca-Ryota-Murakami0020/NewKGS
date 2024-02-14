using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RezalutC : MonoBehaviour
{
    [SerializeField] Material re;
    float value = -1.0f;
    bool push = false;
    public bool PUSH {
        set {
            this.push = false;
        }
        get {
            return this.push;
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

    [SerializeField] RezalutRank rezalut;
    [SerializeField] Text sibariText;
    PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
       re.SetFloat("_Flip", value);
        sibariText.text = "12"; //playerManager.GameLevel.ToString()
    }

    // Update is called once per frame
    void Update()
    {
        if(value <= 1.0f) {
            value+=0.01f;
            re.SetFloat("_Flip", value);
        }

        if(push && rezalut.GO) {
            if(Gamepad.current.bButton.wasPressedThisFrame) {
                yes = true;
                push = false;
            }
        }
    }

    public void OnAnimFin() {
        push = true;
    }
}

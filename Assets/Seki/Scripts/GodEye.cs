using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GodEye : MonoBehaviour
{
    //[SerializeField] GameObject[] godObj;
    // Start is called before the first frame update
    [SerializeField] GameObject godEye;
    void Start()
    {
        /*
        for(int i = 0; i < godObj.Length; i++) {
            godObj[i].SetActive(false);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current.leftShoulder.wasPressedThisFrame) {
            godEye.SetActive(true);
            /*
            for(int i = 0; i < godObj.Length; i++) {
                godObj[i].SetActive(true);
            }
            */
        }
        if(Gamepad.current.leftShoulder.wasReleasedThisFrame) {
            godEye.SetActive(false);
            /*
            for(int i = 0; i < godObj.Length; i++) {
                godObj[i].SetActive(false);
            }
            */
        }
    }
}

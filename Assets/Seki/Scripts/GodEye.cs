using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GodEye : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject godEye;
    [SerializeField] GameObject[] warp;

    void Start()
    {
        godEye.SetActive(false);
       for(int i = 0; i < warp.Length; i++) {
            warp[i].SetActive(false);
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current.leftShoulder.wasPressedThisFrame) {
            godEye.SetActive(true);
            for(int i = 0; i < warp.Length; i++) {
                warp[i].SetActive(true);
            }
        }

        if(Gamepad.current.leftShoulder.wasReleasedThisFrame) {
            godEye.SetActive(false);
            for(int i = 0; i < warp.Length; i++) {
                warp[i].SetActive(false);
            }
        }
    }
}

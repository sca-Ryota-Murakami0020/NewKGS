using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestInput : MonoBehaviour
{
    Vector2 inputMoveVelocity = Vector2.zero;
    private float inputJumpVelocity = 0.0f;
    private float playerSpeed = 0.0f;
    private float jumpPow = 0.0f;

    [SerializeField]
    private ThirdStageGM thirdGM;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = thirdGM.PlayerSpeed;
        jumpPow = thirdGM.PlayerJumpPow;
    }

    // Update is called once per frame
    void Update()
    {
        inputMoveVelocity.x = Input.GetAxis("Horizontal");
        Debug.Log(inputMoveVelocity.x);
        
        this.transform.position += new Vector3(inputMoveVelocity.x, inputJumpVelocity, playerSpeed) * playerSpeed;
    }
}

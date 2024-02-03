using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BousouManager : MonoBehaviour
{
    float missionTime = 120.0f;
    [SerializeField] GameObject[] fazeObj;
    [SerializeField] GameManager gameManager;

  
    int fazeCount = 0;
    [SerializeField] Text bousouText;
    int copyValue;
    [SerializeField] PauseManager pause;
    [SerializeField] ExMissionManager ex;
    [SerializeField] PlayerC playerC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.CurrentRemain != 0 && missionTime <= 0.0f) {
            if(!pause.PAUSE && !playerC.ALLGOAL && ex.EXMISSION)
            missionTime -= Time.deltaTime;
            copyValue = (int)missionTime;
            bousouText.text = copyValue.ToString();
            if(missionTime % 40 == 0) {
                fazeCount++;
                fazeActive(fazeCount);
            }
        } 

        if(!playerC.ALLGOAL && missionTime <= 0f) {
            gameManager.GAMEOVER = true;
        }
    }

    void fazeActive(int c) {
        switch(c) {
            case 1:
                fazeObj[0].SetActive(true);
                break;
            case 2:
                fazeObj[1].SetActive(true);
                break;
            case 3:
                fazeObj[2].SetActive(true);
                break;
        }
    }
}

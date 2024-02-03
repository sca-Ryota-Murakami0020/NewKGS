using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTV : MonoBehaviour
{
    [SerializeField] GameObject[] tvObj;
    [SerializeField] MissionManager missionManager;
    [SerializeField] ExMissionManager exMissionManager;
    [SerializeField] GameObject wideCamera;
    // Start is called before the first frame update
    void Start() {

        tvObj[0].SetActive(true);
        tvObj[1].SetActive(false);
        wideCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if(missionManager.RADOMMISSIONCOUNT == 1) {//停電
            for(int i = 0; i < tvObj.Length; i++) {
                tvObj[i].SetActive(false);
                tvObj[1].SetActive(false);
                wideCamera.SetActive(false);
            }
        }
        //ミッションの暴走ロボットから追いかけられる
        else if(exMissionManager.EXMISSION) {
            tvObj[1].SetActive(true);
            tvObj[0].SetActive(false);
            wideCamera.SetActive(true);
        }
        //通常画
        else {
            tvObj[0].SetActive(true);
            tvObj[1].SetActive(false);
            wideCamera.SetActive(false);
        }
    }
}

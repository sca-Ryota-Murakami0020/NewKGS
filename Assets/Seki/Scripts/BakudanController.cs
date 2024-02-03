using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BakudanController : MonoBehaviour
{
    [SerializeField] MissionManager missionManager;
    int bakudanCount = 0;
    public int BAKUDACOUNT {
        set {
            this.bakudanCount = value;
        }
        get {
            return this.bakudanCount;
        }
    }

    bool bakudan = false;
  
    // Update is called once per frame
    void Update()
    {
        if(bakudanCount >= 3 && !bakudan) {
            missionManager.MISSIONVALUE[missionManager.RADOMMISSIONCOUNT]++;
            missionManager.KeyActive(missionManager.RADOMMISSIONCOUNT);
            missionManager.MiSSIONCOUNT++;
            bakudan = true;
        }

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverController : MonoBehaviour
{
    Animator anim;
    [SerializeField] MissionManager missionManager;
    [SerializeField] GameObject leverUI;
    [SerializeField] PlayerC player;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
        leverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            //player.MISSIO = true;
            anim.enabled = true;
            StartCoroutine(WaitValue());
            
        }
    }

    private void OnTriggerExit(Collider col) {
        if(col.tag == "Player") {
            //leverUI.SetActive(false);
        }
    }

    IEnumerator WaitValue() {
        yield return new WaitForSeconds(1.0f);
        {
            missionManager.MISSIONVALUE[missionManager.RADOMMISSIONCOUNT] = 1;
            missionManager.KeyActive(missionManager.RADOMMISSIONCOUNT);
            missionManager.MiSSIONCOUNT++;
            this.gameObject.SetActive(false);
        }
    }
}

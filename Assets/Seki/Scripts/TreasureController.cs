using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TreasureController : MonoBehaviour
{
    [SerializeField] Animator treasure;
    [SerializeField] Animator item;
    [SerializeField] GameObject treasureObj;
    [SerializeField] int treasureCount;
    [SerializeField] MissionManager mission;
    [SerializeField] PlayerC play;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        treasure.enabled = false;
        item.enabled = false;
        audio= this.GetComponent<AudioSource>();
        audio.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            treasure.enabled = true;
            item.enabled = true;
            audio.enabled = true;
            StartCoroutine(WaitNotActive());
        }
    }

    IEnumerator WaitNotActive() {
        yield return new WaitForSeconds(2.0f);
        if(treasureCount == 1) {
            play.MISSIO = true;
            mission.MISSIONVALUE[mission.RADOMMISSIONCOUNT]++;
            mission.KeyActive(mission.RADOMMISSIONCOUNT);
        }
        treasureObj.SetActive(false);
    }
}

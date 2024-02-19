using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;

public class ExMissionManager : MonoBehaviour
{
    //ミッションのスライドUI
    [SerializeField] GameObject missionUI;
    Animator mission;

    //ミッション内容のテキスト
    [SerializeField] GameObject missionText;

    //ミッションの示唆UI
    [SerializeField] GameObject missionImage;

    //ミッションのオブジェクト(風船とか)
    [SerializeField] GameObject missionObj;

    [SerializeField] PlayerC player;
    [SerializeField] GameObject cinema;
    bool exMission = false;
    public bool EXMISSION {
        set {
            this.exMission = value;
        }
        get {
            return this.exMission;
        }
    }
    bool active = false;
   
    [SerializeField] GameObject[] drive;
    [SerializeField] GameObject[] textMission;
    [SerializeField] Text bigMissionText;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
        mission = missionUI.GetComponent<Animator>();
        mission.enabled = false;
        missionText.SetActive(false);
        missionObj.SetActive(false);
        missionImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.KEYCOUNT >= 3 && !exMission) {
            for(int i = 0; i < drive.Length; i++) {
                drive[i].SetActive(false);
            }
            exMission = true;
            textMission[0].SetActive(false);
            textMission[1].SetActive(true);
            bigMissionText.fontSize = 107;
            bigMissionText.text = "全力でゴールを目指せ!!";
        }

        if(exMission && !active) {
            missionImage.SetActive(true);
            sound.PlayOneShot(music);
            StartCoroutine(WaitActive());
            active = true;
        }
        
        if(Gamepad.current.bButton.wasPressedThisFrame) {
            player.enabled = true;
            cinema.SetActive(true);
            mission.SetBool("book", true);
            StartCoroutine(NotUI());
        }
    }

    IEnumerator NotUI() {
        yield return new WaitForSeconds(1.0f);
        mission.SetBool("book", false);
        mission.enabled = false;
        missionUI.SetActive(false);
    }

    IEnumerator WaitActive() {
        yield return new WaitForSeconds(2.0f);
        MissionActive();
    }
 

    void MissionActive() {
        cinema.SetActive(false);
        player.enabled = false;
        
        missionUI.SetActive(true);
        mission.enabled = true;
        
        missionImage.SetActive(false);
        missionText.SetActive(true);
        missionObj.SetActive(true);
    }
}

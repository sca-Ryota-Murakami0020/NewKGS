using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeAnimFin : MonoBehaviour
{
    [SerializeField] StageSelectController stageSelect;
    [SerializeField] Animator iconAnim;
    [SerializeField] IconController icon;
    [SerializeField] GameObject Siabritukeru;
    [SerializeField] GameObject ModeUi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimFin()
    {
        
        if(!stageSelect.NORMAL && StageSelectController.mode != StageSelectController.MODE.CHALLENGE) {
            icon.enabled = true;
            ModeUi.SetActive(false);
            iconAnim.SetBool("title", true);
        } 
        else if(StageSelectController.mode == StageSelectController.MODE.CHALLENGE) {
            Siabritukeru.SetActive(true);
            stageSelect.enabled = false;
            ModeUi.SetActive(false);
        }
        else {
            
            icon.enabled = false;
        }
    }
}

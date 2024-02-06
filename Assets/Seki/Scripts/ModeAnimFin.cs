using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeAnimFin : MonoBehaviour
{
    [SerializeField] StageSelectController stageSelect;
    [SerializeField] Animator iconAnim;
    [SerializeField] IconController icon;
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
        stageSelect.NOT = true;
        if(!stageSelect.NORMAL) {
            icon.enabled = true;
            iconAnim.SetBool("title", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeAnimFin : MonoBehaviour
{
    [SerializeField] StageSelectController stageSelect;
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
    }
}

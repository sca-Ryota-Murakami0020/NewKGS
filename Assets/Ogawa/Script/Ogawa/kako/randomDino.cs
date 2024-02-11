using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomDino : MonoBehaviour
{
    [SerializeField] GameObject randomEvent1;
    [SerializeField] GameObject randomEvent2;
    [SerializeField] GameObject randomEvent3;

    [SerializeField] KakoPauseManager pauseManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!pauseManager.LOAD) { 
        int randomEvent = Random.Range(1, 4);

        if(randomEvent == 1) {
            randomEvent2.SetActive(false);
            randomEvent3.SetActive(false);
        } else if(randomEvent == 2) {
            randomEvent1.SetActive(false);
            randomEvent3.SetActive(false);
        } else if(randomEvent == 3) {
            randomEvent1.SetActive(false);
            randomEvent2.SetActive(false);
        }
            pauseManager.LOAD = true;
        }
    }
}

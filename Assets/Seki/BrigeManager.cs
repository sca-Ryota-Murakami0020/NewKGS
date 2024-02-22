using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeManager : MonoBehaviour
{
    [SerializeField] GameObject[] brige;
    [SerializeField] GameObject[] pathBrige;
    [SerializeField] PlayerC playerC;
    // Start is called before the first frame update
    void Start()
    {
        for(int u = 0; u < pathBrige.Length; u++) {
            pathBrige[u].SetActive(false);
        }

        for(int i = 0; i < brige.Length; i++) {
            brige[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerC.KEYCOUNT == 3) {
            for(int i = 0; i < brige.Length; i++) {
                brige[i].SetActive(true);
            }
            for(int u = 0; u < pathBrige.Length; u++) {
                pathBrige[u].SetActive(true);
            }
        }
        
    }
}

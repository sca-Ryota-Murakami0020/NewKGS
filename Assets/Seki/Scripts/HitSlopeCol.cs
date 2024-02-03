using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSlopeCol : MonoBehaviour
{
    [SerializeField] KaidanC kaidanC;
    [SerializeField] int slopeCount;
    [SerializeField] int countRall;
    [SerializeField] RallManager rall;
    [SerializeField] BoxCollider[] stopCol;
    [SerializeField] BoxCollider[] slopeCol;
    [SerializeField] GameObject[] Path;
    bool stop = false;
    // Start is called before the first frame update
    void Start() {
        for(int i = 0; i < Path.Length; i++) {
            Path[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            rall.RALLCOUNT = countRall;
            if(slopeCount == 1) {
                Path[0].SetActive(true);
                Path[1].SetActive(false);
                slopeCol[1].enabled = false;
                stopCol[1].enabled = true;
                GoPath(countRall);
                kaidanC.enabled = true;
                kaidanC.ka = true;
                kaidanC.UP = true;
            }
            if(slopeCount == 2) {
                Path[1].SetActive(true);
                Path[0].SetActive(false);
                stopCol[0].enabled = true;
                slopeCol[0].enabled = false;
                RetrunPath(countRall);
                kaidanC.enabled = true;
                kaidanC.ka = true;
                kaidanC.DOWN = true;
            }
        }
    }

    void GoPath(int c) {
        switch(c) {
            case 1:
                kaidanC.DESTPOINT = 0;
                break;
        }
        switch(c) {
            case 2:
                kaidanC.DESTPOINT1 = 0;
                break;
        }
        switch(c) {
            case 3:
                kaidanC.DESTPOINT2 = 0;
                break;
        }
        switch(c) {
            case 4:
                kaidanC.DESTPOINT3 = 0;
                break;
        }
    }

    void RetrunPath(int c) {
        switch(c) {
            case 1:
                kaidanC.DESTPOINT = 1;
                break;
            case 2:
                kaidanC.DESTPOINT1 = 1;
                break;
            case 3:
                kaidanC.DESTPOINT2 = 1;
                break;
            case 4:
                kaidanC.DESTPOINT3= 1;
                break;
        }
    }
}


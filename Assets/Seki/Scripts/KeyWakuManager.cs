using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWakuManager : MonoBehaviour
{
    [SerializeField] PlayerC playerC;
    Animator anim;
    [SerializeField] GameObject[] KeyImage;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < KeyImage.Length; i++) {
            KeyImage[i].SetActive(false);
        }
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerC.GETKEY) {
            anim.SetBool("Key",true);
            StartCoroutine(WaitKey());
        }
        
    }

    IEnumerator WaitKey() {
        yield return new WaitForSeconds(1.0f);
        KeyActive(playerC.KEYCOUNT);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("Key", false);
        playerC.GETKEY = false;
    }

    void KeyActive(int c) {
        if(c != 0) {
            KeyImage[c - 1].SetActive(true);
        }
       
    }

}

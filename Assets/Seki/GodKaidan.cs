using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodKaidan : MonoBehaviour
{
    //ここにLBで神の目が使えるUIを表示する
    [SerializeField] GameObject godUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            //ここにUIを出す
            godUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            //ここにUIを消す
            godUI.SetActive(false);
        }
    }
}

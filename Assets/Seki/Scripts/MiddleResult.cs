using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleResult : MonoBehaviour
{
    [SerializeField]
    private GameObject slideObj;
    SlideUIControl uiCont;
    [SerializeField] PlayerC player;
    // Start is called before the first frame update
    void Start()
    {
        uiCont = slideObj.GetComponent<SlideUIControl>();
        slideObj.SetActive(false);
        player = player.GetComponent<PlayerC>();
    }

    // Update is called once per frame
    void Update()
    {
      
        /*
        if(player.ALLGOAL) {
            uiCont.GAMECLEAR = true;
            slideObj.SetActive(true);
            StartCoroutine("Title");
        }
        */
    }
    private IEnumerator Title() {
        yield return new WaitForSeconds(1.0f);
        uiCont.state = 1;
        //yield return new WaitForSeconds(1.0f);
        //uiCont.state = 2;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Title");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstance : MonoBehaviour
{
    public GameObject shellPrefab;
    //public AudioClip sound;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //Debug.Log(count);
        if(time > 2) {//‚±‚±‚Ìƒ^ƒCƒ€‚Å•p”É‚³‚ª’²ß‚Å‚«‚é
            
            StartCoroutine(PushTama());
            time = 0;
        }

    }

    IEnumerator PushTama() {
      for(int i =0; i <3; i++) {
        GameObject shell = Instantiate(shellPrefab, transform);
        yield return new WaitForSeconds(0.5f);
      }
      StopCoroutine(PushTama());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    float time = 21.0f;
    int timeCopy = 0;
    Text myText;
    AudioSource myaudio;
    // Start is called before the first frame update
    void Start()
    {
        myText = this.GetComponent<Text>();
        myaudio = this.GetComponent<AudioSource>();
        myaudio.pitch = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= 0.0f) {
            time -= Time.deltaTime;
            timeCopy = (int)time;
            myText.text = timeCopy.ToString();
        }
        if(timeCopy == 10) {
            myaudio.pitch = 1.5f;
        }
        
        else if(timeCopy == 0) {
            myaudio.pitch = 1f;
        }
    }
}

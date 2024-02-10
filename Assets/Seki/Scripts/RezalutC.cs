using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RezalutC : MonoBehaviour
{
    [SerializeField] Material re;
    float value = -1.0f;
    // Start is called before the first frame update
    void Start()
    {
       re.SetFloat("_Flip", value);
    }

    // Update is called once per frame
    void Update()
    {
        if(value <= 1.0f) {
            value+=0.01f;
            re.SetFloat("_Flip", value);
        }
    }
}

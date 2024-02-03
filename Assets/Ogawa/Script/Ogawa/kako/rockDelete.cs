using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockDelete : MonoBehaviour
{
    private float count = 0;
    [SerializeField] float deleteTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count+=Time.deltaTime;
        if(count >= deleteTime)
        {
            this.gameObject.SetActive(false);
        }
    }
}

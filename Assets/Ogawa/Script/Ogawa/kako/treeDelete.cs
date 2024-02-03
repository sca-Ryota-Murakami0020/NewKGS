using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeDelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    float Count = 0f;
    void Update()
    {
        if(this.transform.position.y > 0)
        {
            Count += Time.deltaTime;
            if(Count>1f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}

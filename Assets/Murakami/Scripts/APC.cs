using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APC : MonoBehaviour
{
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        x = h;
        z = v;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            y = 10.0f;
        }

        this.transform.position += new Vector3(x,y,z) * _speed * Time.deltaTime;
    }
}

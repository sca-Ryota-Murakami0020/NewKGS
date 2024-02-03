using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var vertical = Input.GetAxis("Vertical") * -1.0f;
        var horizontal = Input.GetAxis("Horizontal") * -1.0f;
        var direction = new Vector3(horizontal, 0.0f, vertical);

        transform.localPosition += direction * (moveSpeed * Time.deltaTime);
        */
        var vertical = Input.GetAxis("Vertical") * -1.0f;
        var horizontal = Input.GetAxis("Horizontal") * -1.0f;
        var direction = new Vector3(0.0f, -vertical,horizontal);

        transform.localPosition += direction * (moveSpeed * Time.deltaTime);
        //transform.localPosition += Vector3.forward * (-Time.deltaTime);
    }
}

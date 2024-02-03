using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHit : MonoBehaviour
{
    /*
    GameObject wall;
    public GameObject WALL {
        
        get {
            return wall.transform.GetChild(0).transform.gameObject;
        }
    }
    public Transform Child {
        get {
            return wall.transform.GetChild(0).transform;
        }
    }

    [SerializeField] Parent parent;
    */
    public Transform target;
    Vector3 offset;
    [SerializeField]
    EmptyHit emptyHit;
    TargetMove2 targetMove2;
    [SerializeField] PlayerC playerC;
    // Start is called before the first frame update
    void Start()
    {
        targetMove2 = this.GetComponent<TargetMove2>();
        //parent.enabled = false;
        offset = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       

        if(emptyHit.WALLFLAG) {
            //this.gameObject.transform.parent = target.gameObject.transform;
            targetMove2.enabled = true;
            //Vector3 direction = new Vector3(270f, 0f, 0f);
            //this.transform.localRotation = Quaternion.Euler(direction);
        }

        else {
            //Vector3 direction = new Vector3(0f, 0f, 0f);
            //this.transform.localRotation = Quaternion.Euler(direction);
            //this.gameObject.transform.parent = null;
            targetMove2.enabled = false;
            this.transform.position = target.position + offset;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Ground") {
            StartCoroutine(WaitFlag());
        }
    }

    IEnumerator WaitFlag() {
        yield return new WaitForSeconds(1.0f);
        emptyHit.WALLFLAG = false;
        //wall = other.gameObject;
        //parent.enabled = true;
        playerC.enabled = true;
    }
}

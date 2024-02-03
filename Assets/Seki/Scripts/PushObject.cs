using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.Equals("Player")) {
            StartCoroutine("GoMovingObject", collision.collider.attachedRigidbody);
        }
    }

    //ÇøÇÂÇ¡Ç∆ÇæÇØâüÇµÇƒÅAé~ÇﬂÇÈ
    private IEnumerator GoMovingObject(Rigidbody inRigid) {
        if(inRigid != null || !inRigid.isKinematic) {
            inRigid.velocity = transform.forward * 1.2f;
            yield return new WaitForSeconds(0.1f);
            inRigid.velocity = Vector3.zero;
        }
    }
}

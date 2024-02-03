using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackButton : MonoBehaviour
{
    Animator anim;
    [SerializeField] Animator Syata;
    SphereCollider sphere;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
        Syata.enabled = false;
        sphere = this.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            sphere.enabled = false;
            anim.enabled = true;
            Syata.enabled = true;
            StartCoroutine(WaitAnim());
        }
    }

    IEnumerator WaitAnim() {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
    }
}

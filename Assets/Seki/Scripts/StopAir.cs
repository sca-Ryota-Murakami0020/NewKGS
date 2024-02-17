using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAir : MonoBehaviour
{
    [SerializeField] AirController airplane;
    public static int count;
    BoxCollider boxCollider;
    [SerializeField] Animator anim;
    [SerializeField] GameObject beam;
    
    //ここにエフェクトのオブジェクトをおく
    // Start is called before the first frame update
    void Start() {
        beam.SetActive(false);
        count = 0;
        boxCollider = this.GetComponent<BoxCollider>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(count);
        if(count == 3) {
            airplane.AIRSPEED = 0.0f;
            //エフェクト再生
            beam.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Player") {
            anim.enabled = true;
            count++;
            boxCollider.enabled = false;
        }
    }
}

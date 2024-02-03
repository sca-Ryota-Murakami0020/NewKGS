using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAir : MonoBehaviour
{
    [SerializeField] AirController airplane;
    public static int count;
    BoxCollider boxCollider;
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem beam;
    //�����ɃG�t�F�N�g�̃I�u�W�F�N�g������
    // Start is called before the first frame update
    void Start() {
        beam.Stop();
        count = 0;
        boxCollider = this.GetComponent<BoxCollider>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if(count == 3) {
            airplane.AIRSPEED = 0.0f;
            //�G�t�F�N�g�Đ�
            beam.Play();
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

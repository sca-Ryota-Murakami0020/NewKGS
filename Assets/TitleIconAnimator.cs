using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleIconAnimator : MonoBehaviour
{
    [SerializeField] IconController icon;
    [SerializeField] Animator printObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimationEnd() {
        //�A�j���[�V�����I�����̏���
        if(!icon.RANK) {
            icon.TITLEFADE = true;
        } else {
            printObj.enabled = true;
            printObj.SetBool("rank",false);
        }
        //Debug.Log("�I�����");
    }
}

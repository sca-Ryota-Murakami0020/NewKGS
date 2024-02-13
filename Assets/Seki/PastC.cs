using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastC : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset;
    Vector3 rotationX;
    [SerializeField] RunOnlyPlayerC runPlayer;
    Camera camera;
    [SerializeField] ThirdStageGM manager;
    // Start is called before the first frame update
    void Start()
    {  
        camera = this.GetComponent<Camera>();
        
        rotationX.x = 26.8f;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(runPlayer.WARP) {
            rotationX.x = 0.0f;
            if(camera.fieldOfView != 0.3f && StageSelectController.mode == StageSelectController.MODE.STORY) {
                camera.fieldOfView -= Time.deltaTime * 30f;
            }
            //else if(camera.fieldOfView <= 0.3f) {
                //manager.NextStage();
            //}
        } 
        if(!runPlayer.GOAL){
            this.transform.position = player.transform.position + offset;
        }
        this.transform.localEulerAngles = rotationX;
        /*
        if(Input.GetKeyDown(KeyCode.U)) {
            runPlayer.GOAL = true;
        }
        */
    }
}

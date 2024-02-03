using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdMove : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private NavMeshAgent agent;
    private int destPoint = 0;
    [SerializeField] GameObject target;
    BulletInstance bullet;
    AudioSource music;
    [SerializeField] PauseManager pause;
    [SerializeField] MissionManager mission;
    // Start is called before the first frame update
    void Start() {
        music = this.GetComponent<AudioSource>();
        music.enabled = false;
        agent = GetComponent<NavMeshAgent>();
        bullet = GetComponentInChildren<BulletInstance>();
        bullet.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        Vector3 cube = target.transform.position;
        float dis = Vector3.Distance(cube, this.transform.position);
        if(!pause.PAUSE && !mission.MISSIONFLAG) {
            agent.speed = 3.5f;
            if(dis < 60f) {
                bullet.enabled = true;
                music.enabled = true;

            } else {
                music.enabled = false;
                bullet.enabled = false;
            }
        } else {
            agent.speed = 0f;
            music.enabled = false;
            bullet.enabled = false;
        }


        if(!agent.pathPending && agent.remainingDistance < 0.3f) {

            GotoNextPoint();
        }

    }

    void GotoNextPoint() {

        if(points.Length == 0) {

            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;

    }
}

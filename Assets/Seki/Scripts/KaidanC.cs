using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.InputSystem;

public class KaidanC : MonoBehaviour
{
    [SerializeField] RallManager rall;
    [SerializeField] PathCreator[] Pos;
    [SerializeField] PathCreator[] Pos1;
    [SerializeField] PathCreator[] Pos2;
    [SerializeField] PathCreator[] Pos3;
    PlayerC playerC;
    PlayerInput player;
    int destPoint = 0;
    public int DESTPOINT {
        set {
            this.destPoint = value;
        }
        get {
            return this.destPoint;
        }
    }

    int destPoint1 = 0;
    public int DESTPOINT1 {
        set {
            this.destPoint1 = value;
        }
        get {
            return this.destPoint1;
        }
    }

    int destPoint2 = 0;
    public int DESTPOINT2 {
        set {
            this.destPoint2 = value;
        }
        get {
            return this.destPoint2;
        }
    }

    int destPoint3 = 0;
    public int DESTPOINT3 {
        set {
            this.destPoint3 = value;
        }
        get {
            return this.destPoint3;
        }
    }

    float speed = 20.0f;
    float d;

    bool up = false;
    public bool UP {
        set {
            this.up = value;
        }
        get {
            return this.up;
        }
    }
    bool down = false;
    public bool DOWN {
        set {
            this.down = value;
        }
        get {
            return this.down;
        }
    }

    public bool ka = false;
    public bool KA {
        set {
            this.ka = value;
        }
        get {
            return this.ka;
        }
    }

    [SerializeField] GameObject suberu;
    [SerializeField] PauseManager pause;
    [SerializeField] MissionManager mission;
    // Start is called before the first frame update
    void Start() {
        playerC = this.GetComponent<PlayerC>();
        player = this.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update() {
        if(!pause.PAUSE && !mission.MISSIONFLAG) { 
        if(up) {
            playerC.enabled = false;
            player.enabled = false;
            suberu.SetActive(true);
            suberu.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = Quaternion.Euler(0, 180f, 0);
            UpMove(rall.RALLCOUNT);
        } else {
           
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(down) {
            suberu.SetActive(true);
            suberu.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = Quaternion.Euler(0, 0f, 0);
            playerC.enabled = false;
            player.enabled = false;
            DownMove(rall.RALLCOUNT);
        } else {
            
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        }
        /*
        GotoNextPoint(destPoint);
        GotoNextPoint1(destPoint1);
        GotoNextPoint2(destPoint2);
        GotoNextPoint3(destPoint3);
        */
    }

    void UpMove(int c) {
        switch(c) {
            case 1:
                RallOne();
                break;
            case 2:
                RallTwo();
                break;
            case 3:
                RallTree();
                break;
            case 4:
                RallFore();
                break;
        }
    }

    void RallOne() {
        d += speed * Time.deltaTime;
        this.transform.position = Pos[destPoint].path.GetPointAtDistance(d);
    }
    void RallTwo() {
        d += speed * Time.deltaTime;
        this.transform.position = Pos1[destPoint1].path.GetPointAtDistance(d);
       
    }
    void RallTree() {
        d += speed * Time.deltaTime;
        this.transform.position = Pos2[destPoint2].path.GetPointAtDistance(d);
    }
    void RallFore() {
        d += speed * Time.deltaTime;
        this.transform.position = Pos2[destPoint2].path.GetPointAtDistance(d);
    }

    void DownMove(int c) {
        switch(c) {
            case 1:
                DownOne();
                break;
            case 2:
                DownTwo();
                break;
            case 3:
                DownTree();
                break;
            case 4:
                DownFore();
                break;
        }
    }

    void DownOne() {
        d += speed * Time.deltaTime;
        this.transform.position = Pos[destPoint].path.GetPointAtDistance(d);
    }

    void DownTwo() {
        d += speed * Time.deltaTime;
        this.transform.position = Pos1[destPoint1].path.GetPointAtDistance(d);
    }

    void DownTree() {
        d += speed * Time.deltaTime;
        this.transform.position = Pos2[destPoint2].path.GetPointAtDistance(d);
    }

    void DownFore() {
        d += speed * Time.deltaTime;
        this.transform.position = Pos3[destPoint3].path.GetPointAtDistance(d);
    }


}

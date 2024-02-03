using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] GameObject[] musicObj;
    string stageMusic;

    MusicManager instance;

    GameManager gameManager;
    PlayerC player;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        for(int i = 0; i < musicObj.Length; i++) {
            musicObj[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        stageMusic = SceneManager.GetActiveScene().name;
        if(stageMusic == "Murakami") {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            player = GameObject.Find("PlayerModel").GetComponent<PlayerC>();
        }
        SelectMusic(SelectStageCount(stageMusic));
    }
    int c;
    int SelectStageCount(string name) {
        switch(name) {
            case "Masaki":
                c = 0;
                break;
            case "Murakami":
                if(gameManager.GAMEOVER) {
                    c = 3;
                }
                else if(player.ALLGOAL) {
                    c = 2;
                }
                else {
                    c = 1;
                }
                break;
        }
        return c;
    }

    public void SelectMusic(int c) {
        for(int i = 0; i < musicObj.Length; i++) {
            if(i == c) {
                musicObj[c].SetActive(true);
            } else {
                musicObj[i].SetActive(false);
            }
        }
    }
}

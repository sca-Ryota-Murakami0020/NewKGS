using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverIcon : MonoBehaviour
{
    RectTransform my;
    [SerializeField] RectTransform[] gameOverPos;
    [SerializeField] Animator gameOverAnim;
    bool gameover = false;
    bool nyuryoku = false;
    string stageName;
    // Start is called before the first frame update
    void Start() {
        my = this.GetComponent<RectTransform>();
        StartCoroutine(StartFlag());
        stageName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update() {
        if(!gameover) {
            IconMove();
            if(my.localPosition == gameOverPos[0].localPosition) {

                if(nyuryoku && Gamepad.current.bButton.wasPressedThisFrame) {
                    gameOverAnim.SetBool("hukki", true);
                    TitleManager.sceneName = stageName;
                    gameover = true;
                    StartCoroutine(WaitScene());
                }

            } else if(my.localPosition == gameOverPos[1].localPosition) {
                new WaitForSeconds(2.0f);
                if(nyuryoku && Gamepad.current.bButton.wasPressedThisFrame) {
                    gameOverAnim.SetBool("idou", true);
                    TitleManager.sceneName = "Masaki";
                    SousaUIContorller.stageClear = 0;
                    gameover = true;
                    StartCoroutine(WaitScene());
                }
            }
        }
    }

    IEnumerator StartFlag() {
        yield return null;
        nyuryoku = true;
    }

    IEnumerator WaitScene() {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("LoadScene");
    }

    void IconMove() {
        if(Gamepad.current.leftStick.left.wasReleasedThisFrame) {
            my.localPosition = gameOverPos[0].localPosition;

        }
        if(Gamepad.current.leftStick.right.wasReleasedThisFrame) {
            my.localPosition = gameOverPos[1].localPosition;

        }
    }
}

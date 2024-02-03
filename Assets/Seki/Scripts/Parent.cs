using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    //順序としては
    //①壁にぶつかった時にぶつかったオブジェクトの子にプレイヤーを設定する
    //②ぶつかった親の子を取得してplayerを子と同じく動かす

   

    [SerializeField] CharacterMove2 characterMove2;

    public Transform target;
    [SerializeField] EmptyHit emptyHit;
    void Start() {
        
        characterMove2.enabled = false;
        //CharacterMove2.target = player.transform.GetChild(0).transform;
    }
    private void Update() {

        if(emptyHit.WALLFLAG) {
            characterMove2.enabled = true;
            //this.gameObject.transform.parent = target.gameObject.transform;
            //Vector3 direction = new Vector3(180f, 0f, 0f);
            //this.transform.localRotation = Quaternion.Euler(direction);
        }

        else {
            characterMove2.enabled = false;
            //this.gameObject.transform.parent = null;
        }
        //player = wallHit.WALL;
        //SetParent(player);
    }

    /*Invoked when a button is pressed.
    public void SetParent(GameObject newParent) {
        //Makes the GameObject "newParent" the parent of the GameObject "player".
        player.transform.parent = newParent.transform;
        Vector3 direction = new Vector3(0f,0f, 90f);
        player.transform.localRotation =Quaternion.Euler(direction);
        characterMove2.enabled = true;//あとでコメントアウト外す
        //Display the parent's name in the console.
        //Debug.Log("Player's Parent: " + player.transform.parent.name);

        // Check if the new parent has a parent GameObject.

    }
    */
    
}

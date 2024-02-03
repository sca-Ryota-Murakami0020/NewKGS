using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemC : MonoBehaviour
{
    [Header("鍵獲得時の獲得スコア"),SerializeField] private int itemScore;
    [Header("アイテム名"),SerializeField] private string itemName;
    private float count = 0.0f;
    [SerializeField] PlayerC pc;

    CapsuleCollider ca;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        ca = this.GetComponent<CapsuleCollider>();
        pc = pc.GetComponent<PlayerC>();
    }

    // Update is called once per frame
    void Update()
    {
        //count += 0.01f;
        //transform.position += new Vector3(0, (Mathf.Sin(count / 50)) * (Time.deltaTime) / 10, 0);
        this.transform.Rotate(0, 0, 0.3f);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player") {
            gameManager.DESSPOS = col.transform.position;
            pc.GetItem(itemScore, itemName);
            ca.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}

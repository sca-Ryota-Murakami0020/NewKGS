using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreasureDistance : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject MissionObject;

    [SerializeField] TMP_Text DistanceText;
    float distance;
    int intDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, MissionObject.transform.position);
        intDistance = (int)distance;
        DistanceText.text = intDistance.ToString() + "m";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpC : MonoBehaviour
{
    [Header("ÉèÅ[ÉvêÊ"),SerializeField]
    private GameObject rollsignPortal;

    public GameObject RollsignPortal
    {
        get { return this.rollsignPortal;}
        set { this.rollsignPortal = value;}
    }
}

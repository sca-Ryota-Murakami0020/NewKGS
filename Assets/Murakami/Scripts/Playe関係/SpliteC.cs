using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpliteC : MonoBehaviour
{
    [Header("加算するスピード"),SerializeField]
    private float addSpeedMag;
    [Header("加算するジャンプ力"),SerializeField]
    private float addJumpMag;
    [Header("軽減する重力"),SerializeField]
    private float subGravity;

    public float AddSpeedMag
    {
        get { return this.addSpeedMag;}
        set { this.addSpeedMag = value;}
    }

    public float AddJumpMag
    {
        get { return this.addJumpMag;}
        set { this.addJumpMag = value;}
    }

    public float SubGravity
    {
        get { return this.subGravity;}
        set { this.subGravity = value;}
    }
}

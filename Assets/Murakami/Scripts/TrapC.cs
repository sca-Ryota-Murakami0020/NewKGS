using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapC : MonoBehaviour
{
    public enum DebufKind
    {
        Null,
        DefMove,
        DefJump,
    }
    [Header("デバフの種類"),SerializeField] 
    private DebufKind debufKind = DebufKind.Null;

    [Header("デバフのレベル"),SerializeField]
    private float debufMag;

    [Header("デバフの付与時間"),SerializeField]
    private int debufTime;

    //プロパティ
    public DebufKind DebufKinds
    {
        get { return this.debufKind;}
    }

    public float DebufMag
    {
        get { return this.debufMag;}
    }

    public int DebufTime
    {
        get { return this.debufTime;}
    }

}

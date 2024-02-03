using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraC : MonoBehaviour
{
    [SerializeField] private int cameraNumber;

    public int CameraNumber
    {
        get { return this.cameraNumber;}
    }
}

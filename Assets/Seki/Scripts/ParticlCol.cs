using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlCol : MonoBehaviour
{
    [SerializeField] AirController airplane;
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "airplane")
        {
            airplane.HITCOUNT++;
            Debug.Log("“–‚½‚Á‚½");
            this.gameObject.SetActive(false);
        }
    }
}

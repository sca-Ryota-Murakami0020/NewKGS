using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankAnim : MonoBehaviour
{
    [SerializeField] MojiManager moji;
    [SerializeField] GameObject my;
    [SerializeField] ParticleSystem particle;

    private void Start() {
        particle.Stop();
    }

    public void OnAnimFin() {
        particle.Play();
        StartCoroutine(waitActive());
    }

    IEnumerator waitActive() {
        yield return new WaitForSeconds(1.0f);
        particle.Stop();
        moji.OnAnimFinish();
        my.SetActive(false);
    }
}

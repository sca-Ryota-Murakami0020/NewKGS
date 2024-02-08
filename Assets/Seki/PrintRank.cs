using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintRank : MonoBehaviour
{
    [SerializeField] IconController icon;
    [SerializeField] Animator titleIcon;
   public void OnFinishAnim() {
        icon.RANK = false;
        titleIcon.SetBool("title", true);
    }
}

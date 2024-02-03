using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    Text letter;
    void Start() {
        letter = GetComponent<Text>();
    }

    public void Selected(bool _selected) {
        if(_selected) {
            arrow.SetActive(true);
        } else {
            arrow.SetActive(false);
        }
    }

    public void SetLetter(string _letter) {
        letter.text = _letter;
    }

    public string SelectLetter() {
        return letter.text;
    }
}

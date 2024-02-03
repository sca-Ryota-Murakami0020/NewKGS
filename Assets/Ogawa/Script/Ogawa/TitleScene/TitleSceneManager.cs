using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] GameObject startButtonUI;
    [SerializeField] GameObject easyButton;
    [SerializeField] GameObject normalButton;
    [SerializeField] GameObject hardButton;
    [SerializeField] GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        

        easyButton.gameObject.SetActive(false);
        normalButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
        Panel.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

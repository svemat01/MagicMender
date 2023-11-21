using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    
    public Transform exitPanel;
    // Start is called before the first frame update
    void Start()
    {
       exitPanel.gameObject.SetActive(false);
            
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            dontLeave();
         
        }





    }

    public void dontLeave()
    {
        PlayerController.Instance.freeze = !PlayerController.Instance.freeze;
        exitPanel.gameObject.SetActive(!exitPanel.gameObject.activeSelf);
    }

    public void leave()
    {
        SceneManager.LoadScene("Menu");
    }
}


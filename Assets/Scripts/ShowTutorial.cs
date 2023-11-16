using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTutorial : MonoBehaviour
{
    public static ShowTutorial instance;

    public GameObject tutorialToShow;
    // Start is called before the first frame update'

    private void Awake()
    {
        // Ensure there is only one instance of ShowTutorial
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

void Start()
    {
      tutorialToShow.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.E) )
        {
          
            
            toggleTutorial();
            

        }
       
    }


    public void toggleTutorial()
    {
        tutorialToShow.SetActive(!tutorialToShow.activeSelf);
        PlayerController.Instance.freeze = tutorialToShow.activeSelf;

    }
}

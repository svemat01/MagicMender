using UnityEngine;
using UnityEngine.SceneManagement; // Add this line

public class ExitOptions : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Update code here (if any)
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            SceneManager.LoadScene("Menu"); // Load the "Menu" scene
        #endif
    }
}
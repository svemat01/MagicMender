using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void StartGame() // Renamed from Start to StartGame
    {
        // Load the game scene
        SceneManager.LoadScene("clintidle");
    }

    public void OpenOptions()
    {
        // Load the options scene
        SceneManager.LoadScene("OptionsUi");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
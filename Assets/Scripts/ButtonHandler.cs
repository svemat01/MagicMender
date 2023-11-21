using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void StartGame() // Renamed from Start to StartGame
    {
        // Load the game scene
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu() // Renamed from Menu to MainMenu
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
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
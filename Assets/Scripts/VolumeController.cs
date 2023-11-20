using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider backgroundMusicSlider;
    public Slider mainCharacterSlider;
    public Slider toolsAndItemsSlider;

    void Start()
    {
        LoadVolumeSettings();
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        mainMixer.SetFloat("BackgroundMusic", volume);
        PlayerPrefs.SetFloat("BackgroundMusic", volume);
    }

    public void SetMainCharacterVolume(float volume)
    {
        mainMixer.SetFloat("MainCharacter", volume);
        PlayerPrefs.SetFloat("MainCharacter", volume);
    }

    public void SetToolsAndItemsVolume(float volume)
    {
        mainMixer.SetFloat("ToolsAndItems", volume);
        PlayerPrefs.SetFloat("ToolsAndItems", volume);
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.Save();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("BackgroundMusic"))
        {
            float backgroundMusicVolume = PlayerPrefs.GetFloat("BackgroundMusic");
            mainMixer.SetFloat("BackgroundMusic", backgroundMusicVolume);
            backgroundMusicSlider.value = backgroundMusicVolume;
        }

        if (PlayerPrefs.HasKey("MainCharacter"))
        {
            float mainCharacterVolume = PlayerPrefs.GetFloat("MainCharacter");
            mainMixer.SetFloat("MainCharacter", mainCharacterVolume);
            mainCharacterSlider.value = mainCharacterVolume;
        }

        if (PlayerPrefs.HasKey("ToolsAndItems"))
        {
            float toolsAndItemsVolume = PlayerPrefs.GetFloat("ToolsAndItems");
            mainMixer.SetFloat("ToolsAndItems", toolsAndItemsVolume);
            toolsAndItemsSlider.value = toolsAndItemsVolume;
        }
    }
}
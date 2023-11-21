using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameoverScreen : MonoBehaviour
{
    public CustomerSpawn spawner;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI errorText;
    
    public TMP_InputField nameInput;

    public GameObject UploadSection;
    public GameObject SuccessSection;
    
    private bool isSubmitting = false;
    
    // Start is called before the first frame update
    void Start()
    {
         
        score = PlayerPrefs.GetInt("score");
        scoreText.text = "You served a total of " + score + " customers!";
        
        UploadSection.SetActive(true);
        SuccessSection.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerPrefs.DeleteKey("score");
    }

    public void NameUpdate()
    {
        if (nameInput.text.Length > 2)
        {
            errorText.text = "";
        } else
        {
            errorText.text = "Please enter a name";
            errorText.color = Color.red;
        }
    }

    public void showHighScore()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Highscores");
    }
    
    public void SubmitScore()
    {
        if (isSubmitting) return;
        
        if (nameInput.text.Length > 2)
        {
            StartCoroutine(SubmitScoreCoroutine());
        } else
        {
            errorText.text = "Please enter a name";
            errorText.color = Color.red;
        }
    }
    
    // Post to https://billions-flag.pockethost.io/api/collections/highscores/records with the following JSON body:
    // {
    //     "name": "Player Name",
    //     "score": 1234
    // }
    
    IEnumerator SubmitScoreCoroutine()
    {
        isSubmitting = true;
        errorText.text = "Submitting...";
        errorText.color = Color.yellow;
        
        string url = "https://billions-flag.pockethost.io/api/collections/highscores/records";
        
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("name", nameInput.text));
        formData.Add(new MultipartFormDataSection("score", score.ToString()));
        
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, formData))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error while submitting highscore: " + webRequest.error + webRequest.downloadHandler.text);
                errorText.text = "Error while submitting highscore: " + webRequest.error;
                errorText.color = Color.red;
            }
            else
            {
                // Deserialize JSON response
                HighscoreItem highscoreItem = JsonUtility.FromJson<HighscoreItem>(webRequest.downloadHandler.text);

                // Process the highscore data as needed
                Debug.Log("Submitted highscore: " + highscoreItem.name + " - " + highscoreItem.score);
                errorText.text = "";

                ShowSuccessSection();

                Invoke("showHighScore", 3);
            }
        }
        
        isSubmitting = false;
    }
    
    public void ShowSuccessSection()
    {
        UploadSection.SetActive(false);
        SuccessSection.SetActive(true);
    }
    
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}

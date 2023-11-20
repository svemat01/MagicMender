using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class HighscoreLoader : MonoBehaviour
{
    public int maxHighscores = 10;
    public TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadHighscores());
    }

    IEnumerator LoadHighscores()
    {
        string url = "https://billions-flag.pockethost.io/api/collections/highscores/records?sort=-score&per-page=" + maxHighscores;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error while fetching highscores: " + webRequest.error);
            }
            else
            {
                // Deserialize JSON response
                HighscoreData highscoreData = JsonUtility.FromJson<HighscoreData>(webRequest.downloadHandler.text);

                // Process the highscore data as needed
                DisplayHighscores(highscoreData.items);
            }
        }
    }

    void DisplayHighscores(HighscoreItem[] highscoreItems)
    {
        // Display highscores in your UI or perform other actions
        // For example, updating a TextMeshProUGUI component:

        string displayText = "";

        for (int i = 0; i < Mathf.Min(maxHighscores, highscoreItems.Length); i++)
        {
            displayText += $"{i + 1}. {highscoreItems[i].name.ToUpper()}: {highscoreItems[i].score}\n";
        }

        highscoreText.text = displayText;
    }
}

[System.Serializable]
public class HighscoreData
{
    public int page;
    public int perPage;
    public int totalItems;
    public int totalPages;
    public HighscoreItem[] items;
}

[System.Serializable]
public class HighscoreItem
{
    public string collectionId;
    public string collectionName;
    public string created;
    public string id;
    public string name;
    public int score;
    public string updated;
}

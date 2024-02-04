using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{
    private string apiUrl = "https://juppet.alwaysdata.net/platformer_game/get_scores.php";
    public GameObject scoreLinePrefab;
    public Transform contentPanel;
    public GameObject scrollView;

    void Start()
    {
        StartCoroutine(LoadScores());
    }

    IEnumerator LoadScores()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erreur lors du chargement des scores : " + www.error);
            }
            else
            {
                ScoresData scoresData = JsonUtility.FromJson<ScoresData>(www.downloadHandler.text);
                DisplayScores(scoresData.playersData);
            }
        }
    }

    void DisplayScores(ScoreData[] playersScores)
    {
        foreach (var playerScore in playersScores)
        {
            GameObject newLine = Instantiate(scoreLinePrefab, contentPanel);
            TextMeshProUGUI[] texts = newLine.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = playerScore.player_name;
            texts[1].text = playerScore.score.ToString();
        }
    }

    public void ToggleScoreboard()
    {
        if (scrollView != null)
        {
            scrollView.SetActive(!scrollView.activeSelf);
        }
        else
        {
            Debug.LogError("ScrollView n'est pas assigné dans l'inspecteur !");
        }
    }
}

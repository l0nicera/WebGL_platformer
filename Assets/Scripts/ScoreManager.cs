using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private string apiUrl = "https://juppet.alwaysdata.net/platformer_game/get_scores.php";
    public SubmitScore scoreSubmitter;
    public TextMeshProUGUI gemText;
    public UnityEvent<ScoreManager> OnGemCollected;
    public TextMeshProUGUI BestGlobalText;
    public TextMeshProUGUI BestPersonnalText;
    private int storedBestPersonalScore;
    private int BestGlobalScore;
    private int currentScore;
    public int NumberOfGems { get; private set; }

    void Start()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Fail fetching scores : " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                Debug.Log("JSON Response: " + jsonResponse);
                ScoresData scoresData = JsonUtility.FromJson<ScoresData>(jsonResponse);
                Debug.Log(scoresData.playersData);

                BestGlobalScore = scoresData.bestGlobal.score;
                BestGlobalText.text = "Best Global: " + BestGlobalScore;
                
                string currentPlayer = PlayerPrefs.GetString("PlayerName", "");
                Debug.Log(currentPlayer);
                
                foreach (var player in scoresData.playersData)
                {
                    if (player.player_name == currentPlayer)
                    {
                        storedBestPersonalScore = player.score;
                        BestPersonnalText.text = "Best Personal: " + storedBestPersonalScore;
                        break;
                    }
                }
            }
        }
    }

public void GemCollected()
    {
        NumberOfGems++;
        UpdateGemText();
        currentScore = NumberOfGems;
         if (currentScore > storedBestPersonalScore)
         {
            BestPersonnalText.text = "Best Personal: " + currentScore ;
            scoreSubmitter.SendScore(PlayerPrefs.GetString("PlayerName", "Anon"), currentScore);
            storedBestPersonalScore = currentScore;
         }
         if (currentScore > BestGlobalScore)
         {
            BestGlobalText.text = "Best Global: " + currentScore;
         }
        OnGemCollected.Invoke(this);
    }

public void UpdateGemText()
{
    gemText.text = NumberOfGems.ToString();
}

public void ResetGems(int count)
    {
        NumberOfGems = count;
        OnGemCollected.Invoke(this);
    }

}

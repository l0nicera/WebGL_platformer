using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SubmitScore : MonoBehaviour
{
    private string apiUrl = "https://juppet.alwaysdata.net/platformer_game/submit_score.php";

    public void SendScore(string playerName, int score)
    {
        StartCoroutine(PostScore(playerName, score));
    }

    IEnumerator PostScore(string playerName, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_name", playerName);
        form.AddField("score", score.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erreur lors de l'envoi du score : " + www.error);
            }
            else
            {
                Debug.Log("Score soumis avec succès : " + www.downloadHandler.text);
            }
        }
    }
}

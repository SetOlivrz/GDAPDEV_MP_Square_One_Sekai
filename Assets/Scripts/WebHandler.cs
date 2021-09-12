using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UI;


public class WebHandler : MonoBehaviour
{
    public string BaseURL
    {
        get
        {
            return "http://my-user-scoreboard.herokuapp.com/api/";
        }
    }

    public void AddToLeaderBoards(InputField inputField)
    {
        Debug.Log("received input");
        int score = (PlayerData.nCollectedSouls * 10) + (PlayerData.nEnemyMonstersKilled * 3) + (PlayerData.nEnemyBossesKilled * 20);
        StartCoroutine(LeaderboardsPostRoutine(inputField.text, score));
    }
    public void CreateGroup()
    {
        StartCoroutine(SamplePostRoutine());
    }

    IEnumerator SamplePostRoutine()
    {
        Dictionary<string, string> GrpParam = new Dictionary<string, string>();

        GrpParam.Add("group_num", "5");
        GrpParam.Add("group_name", "Sekai");
        GrpParam.Add("game_name", "Square One");

        string requestString = JsonConvert.SerializeObject(GrpParam);

        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "groups", "POST");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"status Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Created Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    IEnumerator SampleDeleteRoutine()
    {
        UnityWebRequest request = new UnityWebRequest(BaseURL + "groups/5", "DELETE");

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Response code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");
        }

        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    IEnumerator LeaderboardsPostRoutine(string userName, int score)
    {
        Dictionary<string, string> GrpParam = new Dictionary<string, string>();

        GrpParam.Add("group_num", "5");
        GrpParam.Add("user_name", userName);
        GrpParam.Add("score", score.ToString());

        string requestString = JsonConvert.SerializeObject(GrpParam);

        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        UnityWebRequest request = new UnityWebRequest(BaseURL + "scores", "POST");

        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"status Code: {request.responseCode}");

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Created Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }
}

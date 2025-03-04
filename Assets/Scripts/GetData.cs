using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;

public class GetData : MonoBehaviour
{
    public string DataURL;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(DataURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                Debug.Log(json);
                ReadJSON(json);
            }
        }
    }

    void ReadJSON(string jsonString)
    {
        JSONNode node = JSON.Parse(jsonString);
        JSONObject obj = node.AsObject;
        Debug.Log(obj["near_earth_objects"].Count);
        int numOfAsteroids = obj["near_earth_objects"].Count;

        for (int i = 0; i < numOfAsteroids; i++)
        {
            string isHazardous = obj["near_earth_objects"][i]["is_potentially_hazardous_asteroid"].Value;

            if (isHazardous == "True")
            {
                Debug.Log(obj["near_earth_objects"][i]["name"].Value);
                Debug.Log(obj["near_earth_objects"][i]["estimated_diameter"]["kilometers"]["estimated_diameter_min"].Value);
                Debug.Log(obj["near_earth_objects"][i]["estimated_diameter"]["kilometers"]["estimated_diameter_max"].Value);
            }
        }
    }
}
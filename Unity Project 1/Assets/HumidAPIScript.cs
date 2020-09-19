using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class HumidAPIScript : MonoBehaviour
{
    public GameObject weatherTextObject;
    public GameObject weatherTextObjectT;
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=f3c4a6f6b63f2dbf7d8a94bdd77773d4&units=imperial";

    void Start()
    {

        // wait a couple seconds to start and then refresh every 900 seconds

        InvokeRepeating("GetDataFromWeb", 2f, 900f);
    }

    void GetDataFromWeb()
    {

        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

                int first = webRequest.downloadHandler.text.IndexOf("humidity");
                int second = webRequest.downloadHandler.text.IndexOf("visibility");
                string trim = webRequest.downloadHandler.text.Substring(first, second - first);
                string humid = trim.Trim('h', 'u', 'm', 'i', 'd', 'i', 't', 'y', '"', ':','}',',','}');

                weatherTextObject.GetComponent<TextMeshPro>().text = humid + "%";

                int third = webRequest.downloadHandler.text.IndexOf("temp");
                int fourth = webRequest.downloadHandler.text.IndexOf("feels_like");
                string cut = webRequest.downloadHandler.text.Substring(third, fourth - third);
                string temp = cut.Trim('t', 'e', 'm', 'p', ',', ':', '"', ':', ',');

                weatherTextObjectT.GetComponent<TextMeshPro>().text = temp + " F";
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class WindAPIScript : MonoBehaviour
{
    public GameObject weatherTextObject;
    public GameObject windSock;
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

                int first = webRequest.downloadHandler.text.IndexOf("speed");
                int second = webRequest.downloadHandler.text.IndexOf("deg");
                string trim = webRequest.downloadHandler.text.Substring(first, second - first);
                string windspeed = trim.Trim('s', 'p', 'e', 'e', 'd', '"', ':', '"', ',');

                if (webRequest.downloadHandler.text.Contains("gust"))
                {
                    int third = webRequest.downloadHandler.text.IndexOf("deg");
                    int fourth = webRequest.downloadHandler.text.IndexOf("gust");
                    string trim2 = webRequest.downloadHandler.text.Substring(third, fourth - third);
                    string deg = trim2.Trim('d', 'e', 'g', '"',':',',','"');
                    float num = float.Parse(deg);

                    Quaternion rotation = Quaternion.Euler(90, num, 0);
                    windSock.transform.rotation = rotation;

                    float speedfactor = float.Parse(windspeed);
                    Vector3 scaling = new Vector3(.01f, .01f + (speedfactor * .0001f), .01f);
                    windSock.transform.localScale = scaling;

                    string dir;
                    if (num >= 338 || num <= 22)
                    {
                        dir = "N";
                    }
                    else if (num >= 23 && num <= 67)
                    {
                        dir = "NE";
                    }
                    else if (num >= 68 && num <= 112)
                    {
                        dir = "E";
                    }
                    else if (num >= 113 && num <= 157)
                    {
                        dir = "SE";
                    }
                    else if (num >= 158 && num <= 202)
                    {
                        dir = "S";
                    }
                    else if (num >= 203 && num <= 247)
                    {
                        dir = "SW";
                    }
                    else if (num >= 248 && num <= 292)
                    {
                        dir = "W";
                    }
                    else if (num >= 293 && num <= 337)
                    {
                        dir = "NW";
                    }
                    else
                    {
                        dir = "ERROR";
                    }
                    weatherTextObject.GetComponent<TextMeshPro>().text = windspeed + " mph " + dir;

                }
                else
                {
                    int third = webRequest.downloadHandler.text.IndexOf("deg");
                    int fourth = webRequest.downloadHandler.text.IndexOf("all");
                    string trim2 = webRequest.downloadHandler.text.Substring(third, fourth - third - 2);
                    string deg = trim2.Trim('d', 'e', 'g', ':', '}', ',', '"', 'c', 'l', 'o', 'u', 'd', 's', '"');
                    float num = float.Parse(deg);

                    Quaternion rotation = Quaternion.Euler(90, num, 0);
                    windSock.transform.rotation = rotation;

                    float speedfactor = float.Parse(windspeed);
                    Vector3 scaling = new Vector3(.01f, .01f + (speedfactor * .001f), .01f);
                    windSock.transform.localScale = scaling;

                    string dir;
                    if(num >= 338 || num <= 22)
                    {
                        dir = "N";
                    }
                    else if(num >= 23 && num <= 67)
                    {
                        dir = "NE";
                    }
                    else if(num >= 68 && num <= 112)
                    {
                        dir = "E";
                    }
                    else if(num >= 113 && num <= 157)
                    {
                        dir = "SE";
                    }
                    else if(num >= 158 && num <= 202)
                    {
                        dir = "S";
                    }
                    else if(num >= 203 && num <= 247)
                    {
                        dir = "SW";
                    }
                    else if(num >= 248 && num <= 292)
                    {
                        dir = "W";
                    }
                    else if(num >= 293 && num <= 337)
                    {
                        dir = "NW";
                    }
                    else
                    {
                        dir = "ERROR";
                    }
                    weatherTextObject.GetComponent<TextMeshPro>().text = windspeed + " mph " + dir;

                }

            }
        }
    }
}
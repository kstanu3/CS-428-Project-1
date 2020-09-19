using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class WeatherIconsAPI : MonoBehaviour
{
    public GameObject sunObject;
    public GameObject fewcloudsObject;
    public GameObject scatteredcloudsObject;
    public GameObject brokencloudsObject;
    public GameObject showerrainObject;
    public GameObject rainObject;
    public GameObject thunderstormObject;
    public GameObject snowObject;
    public GameObject mistObject;
    public int weatherIcon = 0;

    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=f3c4a6f6b63f2dbf7d8a94bdd77773d4&units=imperial";

    void Start()
    {

        // wait a couple seconds to start and then refresh every 900 seconds

        InvokeRepeating("GetDataFromWeb", 2f, 900f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            weatherIcon++;
            if(weatherIcon > 9)
            {
                weatherIcon = 1;
            }
            if(weatherIcon == 1)
            {
                brokencloudsObject.SetActive(false);
                thunderstormObject.SetActive(true);
            }
            else if(weatherIcon == 2)
            {
                thunderstormObject.SetActive(false);
                showerrainObject.SetActive(true);
            }
            else if (weatherIcon == 3)
            {
                showerrainObject.SetActive(false);
                rainObject.SetActive(true);
            }
            else if (weatherIcon == 4)
            {
                rainObject.SetActive(false);
                snowObject.SetActive(true);
            }
            else if (weatherIcon == 5)
            {
                snowObject.SetActive(false);
                mistObject.SetActive(true);
            }
            else if (weatherIcon == 6)
            {
                mistObject.SetActive(false);
                sunObject.SetActive(true);
            }
            else if (weatherIcon == 7)
            {
                sunObject.SetActive(false);
                fewcloudsObject.SetActive(true);
            }
            else if (weatherIcon == 8)
            {
                fewcloudsObject.SetActive(false);
                scatteredcloudsObject.SetActive(true);
            }
            else if (weatherIcon == 9)
            {
                scatteredcloudsObject.SetActive(false);
                brokencloudsObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            weatherIcon--;
            if (weatherIcon < 1)
            {
                weatherIcon = 9;
            }
            if (weatherIcon == 1)
            {
                showerrainObject.SetActive(false);
                thunderstormObject.SetActive(true);
            }
            else if (weatherIcon == 2)
            {
                rainObject.SetActive(false);
                showerrainObject.SetActive(true);
            }
            else if (weatherIcon == 3)
            {
                snowObject.SetActive(false);
                rainObject.SetActive(true);
            }
            else if (weatherIcon == 4)
            {
                mistObject.SetActive(false);
                snowObject.SetActive(true);
            }
            else if (weatherIcon == 5)
            {
                sunObject.SetActive(false);
                mistObject.SetActive(true);
            }
            else if (weatherIcon == 6)
            {
                fewcloudsObject.SetActive(false);
                sunObject.SetActive(true);
            }
            else if (weatherIcon == 7)
            {
                scatteredcloudsObject.SetActive(false);
                fewcloudsObject.SetActive(true);
            }
            else if (weatherIcon == 8)
            {
                brokencloudsObject.SetActive(false);
                scatteredcloudsObject.SetActive(true);
            }
            else if (weatherIcon == 9)
            {
                thunderstormObject.SetActive(false);
                brokencloudsObject.SetActive(true);
            }
        }

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

                int first = webRequest.downloadHandler.text.IndexOf("id");
                int second = webRequest.downloadHandler.text.IndexOf("main");
                string trim = webRequest.downloadHandler.text.Substring(first, second - first);
                string text = trim.Trim('i', 'd', ':',',','"');
                int num = int.Parse(text);

                if (num >= 200 && num <= 232)
                {
                    weatherIcon = 1;
                    thunderstormObject.SetActive(true);
                }
                else if (num >= 300 && num <= 321 || num >= 520 && num <= 531)
                {
                    weatherIcon = 2;
                    showerrainObject.SetActive(true);
                }
                else if (num >= 500 && num <= 504)
                {
                    weatherIcon = 3;
                    rainObject.SetActive(true);
                }
                else if (num == 511 || (num >= 600 && num <= 622))
                {
                    weatherIcon = 4;
                    snowObject.SetActive(true);
                }
                else if (num >= 701 && num <= 781)
                {
                    weatherIcon = 5;
                    mistObject.SetActive(true);
                }
                else if (num == 800)
                {
                    weatherIcon = 6;
                    sunObject.SetActive(true);
                }
                else if (num == 801)
                {
                    weatherIcon = 7;
                    fewcloudsObject.SetActive(true);
                }
                else if (num == 802)
                {
                    weatherIcon = 8;
                    scatteredcloudsObject.SetActive(true);
                }
                else if (num == 803 || num == 804)
                {
                    weatherIcon = 9;
                    brokencloudsObject.SetActive(true);
                }
                else
                {
                    text = "ERROR";
                }


            }
        }
    }
}
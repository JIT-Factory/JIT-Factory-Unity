using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using SimpleJSON;

public class Product1Btn : MonoBehaviour
{
    private string _status = "stop"; // 현재 status 값
    public bool ProPlayButton1;
    private string _url = "http://localhost:8080/api/orders/new";

    // Start is called before the first frame update
    void Awake()
    {
        ProPlayButton1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void ProButton1()
    {
        ProPlayButton1 = !ProPlayButton1;
        Debug.Log("btn1");

        // 현재 상태에 따라 status 값 설정
        if (ProPlayButton1)
        {
            _status = "run";
        }
        else
        {
            _status = "stop";
        }

        // 데이터 생성
        var data = new Dictionary<string, string>
        {
            { "productName", "ProductA" },
            { "status", _status }
        };

        // HTTP POST 요청
        using (var client = new HttpClient())
        {
            var content = new FormUrlEncodedContent(data);
            var response = await client.PostAsync(_url, content);

            if (response.IsSuccessStatusCode)
            {
                Debug.Log("POST success");
            }
            else
            {
                Debug.Log("POST failed: " + response.StatusCode);
            }
        }
    }

    public bool GetProButton1()
    {
        return ProPlayButton1;
    }
    
    public void SetProButton1(bool value)
    {
        ProPlayButton1 = value;
    }
}
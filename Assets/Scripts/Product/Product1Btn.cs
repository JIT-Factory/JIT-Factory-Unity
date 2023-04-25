using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using SimpleJSON;
using System.Text;

public class Product1Btn : MonoBehaviour
{
    private string _status = "stop"; // ���� status ��
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

        // ���� ���¿� ���� status �� ����
        if (ProPlayButton1)
        {
            _status = "run";
        }
        else
        {
            _status = "stop";
        }

        // ������ ����
        var json = JSON.Parse("{}");
        json["productName"] = "ProductA";
        json["status"] = _status;

        // HTTP POST ��û
        using (var client = new HttpClient())
        {
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
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
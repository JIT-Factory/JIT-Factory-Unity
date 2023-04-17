using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using SimpleJSON;
using System.Collections.Generic;
public class ApiAuthLogin : MonoBehaviour
{
    private bool _shouldContinuouslyReceive = true;

    async void Start()
    {
        // Step 1: Authenticate and get token
        string token = await GetTokenAsync("ddd@dd.dd", "dd");
        Debug.Log($"Token: {token}");

        // Step 2: Continuously receive data with the token
        while (_shouldContinuouslyReceive)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/orders/all");
            Debug.Log("Response received");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Debug.Log(jsonResponse);

                JSONArray jsonArray = JSON.Parse(jsonResponse).AsArray;

                foreach (JSONNode jsonNode in jsonArray)
                {
                    Order order = JsonUtility.FromJson<Order>(jsonNode.ToString());

                    if (order.productName == "ProductA")
                    {
                        
                        Product1Btn product1Btn = GameObject.Find("Product1").GetComponent<Product1Btn>();
                        if (order.status == "run")
                        {
                            product1Btn.SetProButton1(true);
                        }
                        else
                        {
                            product1Btn.SetProButton1(false);
                        }
                    }
                }
            }
            else
            {
                Debug.Log(response.StatusCode.ToString());
            }

            await Task.Delay(1000);
        }
    }

    private void OnDisable()
    {
        _shouldContinuouslyReceive = false;
    }

    private async Task<string> GetTokenAsync(string email, string password)
    {
        var values = new Dictionary<string, string>
        {
            { "email", email },
            { "password", password }
        };

        var content = new FormUrlEncodedContent(values);

        using (var client = new HttpClient())
        {
            var response = await client.PostAsync("http://localhost:8080/api/auth/login", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var json = JSON.Parse(responseString);
            return json["token"].Value;
        }
    }

    private class Order
    {
        public int id;
        public string productName;
        public string status;
    }
}
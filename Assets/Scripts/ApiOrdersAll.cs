using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using SimpleJSON;

public class ApiOrdersAll : MonoBehaviour
{
    private bool _shouldContinuouslyReceive = true;
    private string _previousJsonResponse = "";

    async void Start()
    {
        while (_shouldContinuouslyReceive)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/orders/all");
            Debug.Log("Response received");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                Debug.Log(jsonResponse);

                if (jsonResponse != _previousJsonResponse)  // Check if the received data is different from the previous data
                {
                    _previousJsonResponse = jsonResponse;

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

    private class Order
    {
        public int id;
        public string productName;
        public string status;
    }
}
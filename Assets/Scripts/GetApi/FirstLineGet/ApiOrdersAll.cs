using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using SimpleJSON;

public class ApiOrdersAll : MonoBehaviour
{
    private bool _shouldContinuouslyReceive = true;
    private string _previousJsonResponse = "";
    GameObject conveyorBelt;
    public GameObject volvoCar;
    public GameObject spawn;
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

                        if (order.productName == "Car")
                        {
                            conveyorBelt  = GameObject.Find("ConveyorManagement");
                            
                            if (order.status == "run")
                            {
                                conveyorBelt.GetComponent<PlayMachine>().MachineOperationConTrue();
                                Debug.Log("get받아서 True");
                                Instantiate(volvoCar,spawn.transform.position , Quaternion.identity);
                                
                            }
                            else
                            {
                               conveyorBelt.GetComponent<PlayMachine>().MachineOperationConFalse();
                                Debug.Log("get받아서 false");
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
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    private string _url = "http://localhost:8080/api/auth/login";
    private string _email = "ddd@dd.dd";
    private string _password = "dd";
    private string _accessToken = "";
    private string _refreshToken = "";

    void Start()
    {
        // �α��� ��û ������
        StartCoroutine(Login());
    }

    IEnumerator Login()
{
    // �α��ο� �ʿ��� ������ ����
    var data = new Dictionary<string, string>
    {
        { "email", _email },
        { "password", _password }
    };

    // HTTP POST ��û ������
    using (var request = new UnityWebRequest(_url, "POST"))
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // ��û ����� ���� ���� �߰�
        request.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(_email + ":" + _password)));
        
        // ��û ����� Content-Type �߰�
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // �α��� ���� ��, AccessToken�� RefreshToken ���� ����
            var responseJson = JSON.Parse(request.downloadHandler.text);
            _accessToken = responseJson["accessToken"];
            _refreshToken = responseJson["refreshToken"];
        }
        else
        {
            Debug.Log("�α��� ����: " + request.error);
        }
    }
}
    // AccessToken �� ��ȯ
    public string GetAccessToken()
    {
        return _accessToken;
    }

    // RefreshToken �� ��ȯ
    public string GetRefreshToken()
    {
        return _refreshToken;
    }
}
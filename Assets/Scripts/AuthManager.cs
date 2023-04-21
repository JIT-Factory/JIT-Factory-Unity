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
        // 로그인 요청 보내기
        StartCoroutine(Login());
    }

    IEnumerator Login()
{
    // 로그인에 필요한 데이터 생성
    var data = new Dictionary<string, string>
    {
        { "email", _email },
        { "password", _password }
    };

    // HTTP POST 요청 보내기
    using (var request = new UnityWebRequest(_url, "POST"))
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // 요청 헤더에 인증 정보 추가
        request.SetRequestHeader("Authorization", "Basic " + System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(_email + ":" + _password)));
        
        // 요청 헤더에 Content-Type 추가
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // 로그인 성공 시, AccessToken과 RefreshToken 값을 저장
            var responseJson = JSON.Parse(request.downloadHandler.text);
            _accessToken = responseJson["accessToken"];
            _refreshToken = responseJson["refreshToken"];
        }
        else
        {
            Debug.Log("로그인 실패: " + request.error);
        }
    }
}
    // AccessToken 값 반환
    public string GetAccessToken()
    {
        return _accessToken;
    }

    // RefreshToken 값 반환
    public string GetRefreshToken()
    {
        return _refreshToken;
    }
}
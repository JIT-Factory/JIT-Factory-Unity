using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;

public class SendMainCameraImage : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 1. RenderTexture 생성
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
            mainCamera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

            // 2. Texture2D 생성
            mainCamera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            mainCamera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            // 3. Texture2D를 바이트 배열로 변환
            byte[] bytes = screenShot.EncodeToJPG();

            // 4. 바이트 배열을 Json으로 인코딩
            string json = JsonConvert.SerializeObject(bytes);

            // 5. Json 파일에 쓰거나 네트워크를 통해 전송
            StartCoroutine(PostScreenshot(json));
            Debug.Log(json);
        }
    }

    IEnumerator PostScreenshot(string json)
    {
        // 1. HTTP 요청 생성
        string url = "http://localhost:8080/api/screenshots";
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        
        // 2. HTTP 요청 보내기
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Screenshot uploaded!");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;


public class WebcamAPI : MonoBehaviour
{
    private Camera mainCamera;
    //private string Path = "./";
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 스페이스바를 누르면 서버로 이미지 데이터를 전송
            StartCoroutine(PostRequest());
        }
    }

    IEnumerator PostRequest()
{
    // 요청을 보낼 URL
    string url = "http://localhost:8080/api/screenshot";

    // 스크린샷을 가져와 JPEG 이미지로 변환
    Texture2D screenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();
    byte[] imageData = screenshotTexture.EncodeToJPG();

    // 요청 생성
    UnityWebRequest www = UnityWebRequest.Post(url, "");
    www.uploadHandler = new UploadHandlerRaw(imageData);
    www.SetRequestHeader("Content-Type", "image/jpeg");

    // 요청 보내기
    yield return www.SendWebRequest();
    
    if (www.result != UnityWebRequest.Result.Success)
    {
        Debug.Log("Error sending screenshot: " + www.error);
        string path = "C:/uploads/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            File.WriteAllBytes(path, imageData);
            Debug.Log("Image saved to " + path);
    }
    else
    {
        // JPEG 이미지 파일 생성 및 로컬 저장
        
    }
}
}
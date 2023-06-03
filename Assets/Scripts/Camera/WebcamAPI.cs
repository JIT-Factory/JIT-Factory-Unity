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
            // �����̽��ٸ� ������ ������ �̹��� �����͸� ����
            StartCoroutine(PostRequest());
        }
    }

    IEnumerator PostRequest()
{
    // ��û�� ���� URL
    string url = "http://localhost:8080/api/screenshot";

    // ��ũ������ ������ JPEG �̹����� ��ȯ
    Texture2D screenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();
    byte[] imageData = screenshotTexture.EncodeToJPG();

    // ��û ����
    UnityWebRequest www = UnityWebRequest.Post(url, "");
    www.uploadHandler = new UploadHandlerRaw(imageData);
    www.SetRequestHeader("Content-Type", "image/jpeg");

    // ��û ������
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
        // JPEG �̹��� ���� ���� �� ���� ����
        
    }
}
}
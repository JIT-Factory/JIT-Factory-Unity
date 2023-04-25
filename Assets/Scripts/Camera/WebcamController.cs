using UnityEngine;

public class WebcamController : MonoBehaviour
{
    public WebCamTexture webcamTexture;
     public Camera[] cameras;
    private int currentCameraIndex = 0;

    private void Start()
    {
        // ��� ī�޶� ����, ù ��° ī�޶� �մϴ�.
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // ��� ������ ��ķ ��ġ ����� �����ɴϴ�.
        WebCamDevice[] devices = WebCamTexture.devices;

        // ù ��° ��ķ�� ����մϴ�.
        if (devices.Length > 0)
        {
            cameras[currentCameraIndex].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            webcamTexture = new WebCamTexture(devices[0].name);
            webcamTexture.Play();
        }
    }

    private void Update()
    {
        // �����̽��ٸ� ������ ī�޶� �����մϴ�.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        // ���� ī�޶� ����, ���� ī�޶� �մϴ�.
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex++;
        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = 0;
        }
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // ���� ī�޶��� targetTexture�� �����Ͽ�, �ش� ī�޶� ������ ȭ���� �ٸ� ��ü�� �������մϴ�.
        cameras[currentCameraIndex].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        Material material = GetComponent<Renderer>().material;
        material.mainTexture = cameras[currentCameraIndex].targetTexture;
    }
}
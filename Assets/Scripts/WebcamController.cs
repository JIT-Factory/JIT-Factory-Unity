using UnityEngine;

public class WebcamController : MonoBehaviour
{
    public WebCamTexture webcamTexture;
     public Camera[] cameras;
    private int currentCameraIndex = 0;

    private void Start()
    {
        // 모든 카메라를 끄고, 첫 번째 카메라를 켭니다.
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // 사용 가능한 웹캠 장치 목록을 가져옵니다.
        WebCamDevice[] devices = WebCamTexture.devices;

        // 첫 번째 웹캠을 사용합니다.
        if (devices.Length > 0)
        {
            cameras[currentCameraIndex].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            webcamTexture = new WebCamTexture(devices[0].name);
            webcamTexture.Play();
        }
    }

    private void Update()
    {
        // 스페이스바를 누르면 카메라를 변경합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        // 현재 카메라를 끄고, 다음 카메라를 켭니다.
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex++;
        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = 0;
        }
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // 현재 카메라의 targetTexture를 변경하여, 해당 카메라가 보내는 화면을 다른 객체로 렌더링합니다.
        cameras[currentCameraIndex].targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        Material material = GetComponent<Renderer>().material;
        material.mainTexture = cameras[currentCameraIndex].targetTexture;
    }
}
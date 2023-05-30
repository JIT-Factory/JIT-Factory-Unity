using UnityEngine;

public class WebcamController : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex;

    private void Start()
    {
        currentCameraIndex = 0;
        SetActiveCamera(currentCameraIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        // 현재 카메라를 비활성화
        cameras[currentCameraIndex].enabled = false;
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = false;

        // 다음 카메라 인덱스로 전환
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // 새로운 카메라를 활성화
        SetActiveCamera(currentCameraIndex);
    }

    private void SetActiveCamera(int index)
    {
        cameras[index].enabled = true;
        cameras[index].GetComponent<AudioListener>().enabled = true;

        // 카메라가 현재 위치를 볼 수 있도록 설정
        Camera.main.transform.position = cameras[index].transform.position;
        Camera.main.transform.rotation = cameras[index].transform.rotation;
    }
}

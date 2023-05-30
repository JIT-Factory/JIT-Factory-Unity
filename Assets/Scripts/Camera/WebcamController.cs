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
        // ���� ī�޶� ��Ȱ��ȭ
        cameras[currentCameraIndex].enabled = false;
        cameras[currentCameraIndex].GetComponent<AudioListener>().enabled = false;

        // ���� ī�޶� �ε����� ��ȯ
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // ���ο� ī�޶� Ȱ��ȭ
        SetActiveCamera(currentCameraIndex);
    }

    private void SetActiveCamera(int index)
    {
        cameras[index].enabled = true;
        cameras[index].GetComponent<AudioListener>().enabled = true;

        // ī�޶� ���� ��ġ�� �� �� �ֵ��� ����
        Camera.main.transform.position = cameras[index].transform.position;
        Camera.main.transform.rotation = cameras[index].transform.rotation;
    }
}

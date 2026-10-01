using UnityEngine;
using Unity.Cinemachine;

public class CameraZoneSwitch : MonoBehaviour
{
    public string triggerTag;
    
    public CinemachineCamera primaryCamera;

    public CinemachineCamera[] virtualCameras;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchToCamera(primaryCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            CinemachineCamera targetCamera = other.GetComponentInChildren<CinemachineCamera>();
            SwitchToCamera(targetCamera);
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag(triggerTag))
    //     {
    //         SwitchToCamera(primaryCamera);
    //     }
    // }

    private void SwitchToCamera(CinemachineCamera targetCamera)
    {
        foreach (CinemachineCamera virtualCamera in virtualCameras)
        {
            virtualCamera.enabled = virtualCamera == targetCamera;
        }
    }
}

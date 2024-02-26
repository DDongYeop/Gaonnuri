using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [Header("Camera")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _PlayerCamera;
    [SerializeField] private List<CinemachineVirtualCamera> _virtualCameras;

    public void PlayCameraPriority(int value)
    {
        _PlayerCamera.depth = value;
        UIManager.Instance.SetUICanvas(UICanvasState.PLAYER);
    }
}

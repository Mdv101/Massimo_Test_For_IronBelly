using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speedOfSpin;
    [SerializeField] private CinemachineVirtualCamera camera;

    private bool rotationState;
    private Tween rotationTween;
    
    private void Awake()
    {
        StartRotation();
    }
    
    /// <summary>
    /// Change Active/Deactivate rotation of camera, return bool with current state
    /// </summary>
    /// <returns></returns>
    public bool ChangeRotationState()
    {
        if (rotationState)
        {
            StopRotation();
        }
        else
        {
            StartRotation();
        }

        return rotationState;
    }

    private void StartRotation()
    {
        rotationState = true;
        rotationTween = transform.DORotate(new Vector3(0, 360, 0), 1f *speedOfSpin, RotateMode.FastBeyond360).
            SetEase(Ease.Linear).
            SetLoops(-1);
    }

    private void StopRotation()
    {
        rotationState = false;
        rotationTween.Kill();
    }

    public void ZoomIn()
    {
         camera.m_Lens.FieldOfView--;
    }

    public void ZoomOut()
    {
        camera.m_Lens.FieldOfView++;
    }
}

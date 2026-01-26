using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;

    [Header("Ω√¡°")]
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 7.3f, -17f);
    [SerializeField] private float _cameraLookAtHeight = -9.0f;

    private Transform _camTr;

    private void Awake()
    {
        if (_camera == null)
        {
            GameObject mainCamGO = GameObject.FindGameObjectWithTag("MainCamera");

            if (mainCamGO != null)
            {
                _camera = mainCamGO.GetComponent<Camera>();
            }
        }

        if (_target == null || _camera == null)
        {
            enabled = false;
            return;
        }
    }
    void Start()
    {
        _camTr = _camera.transform;
        InitSetting();
    }

    void InitSetting()
    {
        Vector3 desiredPos;
        Quaternion desiredRot;

        BuildCameraPose(out desiredPos, out desiredRot);

        ApplyPose(desiredPos, desiredRot);
    }

    void ApplyPose(Vector3 desiredPos, Quaternion desiredRot)
    {
        _camTr.position = desiredPos;
        _camTr.rotation = desiredRot;

    }

    private void BuildCameraPose(out Vector3 desiredPos, out Quaternion desiredRot)
    {
        desiredPos = _target.position + _cameraOffset;

        Vector3 lookPos = _target.position + Vector3.up * _cameraLookAtHeight;
        desiredRot = Quaternion.LookRotation(lookPos - desiredPos, Vector3.up);
    }
}

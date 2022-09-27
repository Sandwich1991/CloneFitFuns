using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;

    public Vector3 deltaPos = new Vector3(0, 7, -5);

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        _camera.transform.position = Managers.Player.PlayerPos + deltaPos;
        _camera.transform.LookAt(Managers.Player.PlayerTransform);
    }
}

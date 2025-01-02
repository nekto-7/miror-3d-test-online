using UnityEngine;
using Mirror;
public class CameraController : NetworkBehaviour
{
    public float sensitivity = 2;
    public float smoothing = 1.5f;
    public float distanceCamera = 0;
    [SerializeField] 
    private Transform _tr;
    private Vector2 velocity;
    private Vector2 frameVelocity;   
    void Reset()
    {
        _tr = GetComponentInParent<CameraController>().transform;
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        _tr.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
    void LateUpdate()
    {
        if (!isLocalPlayer) return;

        transform.position = _tr.position + Vector3.up * distanceCamera; // 1.5f - это примерное расстояние от головы до камеры
        transform.rotation = _tr.rotation;

        transform.Rotate(Vector3.right, velocity.y);
        _tr.Rotate(Vector3.up, velocity.x);

        frameVelocity = Vector2.zero;
    }
}
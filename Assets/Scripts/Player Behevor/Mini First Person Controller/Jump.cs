using UnityEngine;
using Mirror;

public class Jump : NetworkBehaviour

{
    private Rigidbody _rb;
    private GroundCheck _gc;
    
    public event System.Action Jumped;
    public float jumpStrength = 2;

    private void Reset()
    {
        _gc = GetComponentInChildren<GroundCheck>();
    }
    private void Update()
    {
        if (!isLocalPlayer) return;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && (!_gc || _gc.isGrounded))
        {
            _rb.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}

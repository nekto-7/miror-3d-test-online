using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MoveController : NetworkBehaviour
{
    public float speed = 5;
    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    private Rigidbody _rb;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       if (!isLocalPlayer) return;
    }
    void FixedUpdate()
    {
         
        IsRunning = canRun && Input.GetKey(runningKey);
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        _rb.velocity = transform.rotation * new Vector3(targetVelocity.x, _rb.velocity.y, targetVelocity.y);
    }
}
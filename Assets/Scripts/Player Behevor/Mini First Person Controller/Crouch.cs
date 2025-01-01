using UnityEngine;
using Mirror;

public class Crouch : NetworkBehaviour
{
    public KeyCode key = KeyCode.LeftControl;
    public MoveController move;
    public float movementSpeed = 2;

    public Transform headToLower;
    public float? defaultHeadYLocalPosition;
    public float crouchYHeadPosition = 1;    
    public CapsuleCollider colliderToLower;
    
    [HideInInspector]
    public float? defaultColliderHeight;

    public bool IsCrouched { get; private set; }
    public event System.Action CrouchStart, CrouchEnd;

    void Reset()
    {
        move = GetComponentInParent<MoveController>();
        headToLower = move.GetComponentInChildren<Camera>().transform;
        colliderToLower = move.GetComponentInChildren<CapsuleCollider>();
    }

    void LateUpdate()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKey(key))
        {
            
            if (headToLower)
            {
                if (!defaultHeadYLocalPosition.HasValue)
                {
                    defaultHeadYLocalPosition = headToLower.localPosition.y;
                }

                headToLower.localPosition = new Vector3(headToLower.localPosition.x, crouchYHeadPosition, headToLower.localPosition.z);
            }

            if (colliderToLower)
            {
                if (!defaultColliderHeight.HasValue)
                {
                    defaultColliderHeight = colliderToLower.height;
                }

                float loweringAmount;
                if(defaultHeadYLocalPosition.HasValue)
                {
                    loweringAmount = defaultHeadYLocalPosition.Value - crouchYHeadPosition;
                }
                else
                {
                    loweringAmount = defaultColliderHeight.Value * .5f;
                }

                colliderToLower.height = Mathf.Max(defaultColliderHeight.Value - loweringAmount, 0);
                colliderToLower.center = Vector3.up * colliderToLower.height * .5f;
            }

            if (!IsCrouched)
            {
                IsCrouched = true;
                SetSpeedOverrideActive(true);
                CrouchStart?.Invoke();
            }
        }
        else
        {
            if (IsCrouched)
            {
                if (headToLower)
                {
                    headToLower.localPosition = new Vector3(headToLower.localPosition.x, defaultHeadYLocalPosition.Value, headToLower.localPosition.z);
                }

                if (colliderToLower)
                {
                    colliderToLower.height = defaultColliderHeight.Value;
                    colliderToLower.center = Vector3.up * colliderToLower.height * .5f;
                }

                IsCrouched = false;
                SetSpeedOverrideActive(false);
                CrouchEnd?.Invoke();
            }
        }
    }
    private void Update()
    {
       if (!isLocalPlayer) return;
    }

    #region Speed override.
    void SetSpeedOverrideActive(bool state)
    {
        if(!move)
        {
            return;
        }

        if (state)
        {
            if (!move.speedOverrides.Contains(SpeedOverride))
            {
                move.speedOverrides.Add(SpeedOverride);
            }
        }
        else
        {
           if (move.speedOverrides.Contains(SpeedOverride))
            {
                move.speedOverrides.Remove(SpeedOverride);
            }
        }
    }

    float SpeedOverride() => movementSpeed;
    #endregion
}

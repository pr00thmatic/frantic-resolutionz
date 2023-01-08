using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class MotionControlz : MonoBehaviour {
  [Header("Configuration")]
  public float maxSpeed = 5;
  public InputAction horizontalControlz;
  public Rigidbody body;

  [Header("Information")]
  public float desiredHorizontal;
  public int orientation = 1;
  public bool isBlockedLeft;
  public bool isBlockedRight;
  public float motion = 0;

  [Header("Initialization")]
  public Animator animatorz;
  public Animator rootAnimatorz;
  public Transform horizontalRaycast;

  void OnEnable () {
    horizontalControlz.Enable();
    rootAnimatorz.SetInteger("orientation", orientation);
    animatorz.SetTrigger("turn");
  }

  void Update () {
    isBlockedLeft = RaycastSide(-Vector3.right);
    isBlockedRight = RaycastSide(Vector3.right);
    desiredHorizontal = horizontalControlz.ReadValue<float>();

    if ((desiredHorizontal < 0 && !isBlockedLeft) ||
        (desiredHorizontal > 0 && !isBlockedRight)) {
      motion = desiredHorizontal;
    } else {
      motion = 0;
    }

    if (desiredHorizontal != 0 && Mathf.Sign(desiredHorizontal) != orientation) {
      orientation = (int) Mathf.Sign(desiredHorizontal);
      rootAnimatorz.SetInteger("orientation", orientation);
      animatorz.SetTrigger("turn");
    }

    body.velocity = Utilz.SetX(body.velocity, motion * maxSpeed);
    animatorz.SetFloat("speed x", Mathf.Abs(motion));
  }

  bool RaycastSide (Vector3 orientation) {
    return Physics.BoxCast(horizontalRaycast.position,
                           horizontalRaycast.GetChild(0).localPosition.y * Vector3.one,
                           orientation, Quaternion.identity,
                           horizontalRaycast.GetChild(0).localPosition.x,
                           LayerMask.GetMask("Floorz"));
  }
}

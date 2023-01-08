using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Airbornez : MonoBehaviour {
  [Header("Configuration")]
  public float jumpHeight;
  public InputAction jumpControlz;
  public int totalHops = 2;

  [Header("Information")]
  public bool isAirborne = false;
  public RaycastHit hit;
  public float Radius { get => collider.radius; }
  public float jumpPerformedTimestamp = 0;
  public float availableHops = 2;

  [Header("Initialization")]
  public Rigidbody body;
  public Animator animator;
  public Transform raycast;
  public new CapsuleCollider collider;

  void OnEnable () {
    jumpControlz.Enable();
    jumpControlz.started += OnJumpRequested;
  }

  void FixedUpdate () {
    isAirborne = !Physics.SphereCast(raycast.position, Radius * 0.9f, -Vector3.up, out hit,
                                     raycast.GetChild(0).localPosition.magnitude, LayerMask.GetMask("Floorz"));
    if (!isAirborne) {
      availableHops = totalHops;
    }

    animator.SetBool("is airborne", isAirborne || Time.time - jumpPerformedTimestamp < 0.25f);
  }

  public void OnJumpRequested (InputAction.CallbackContext ctx) {
    if (!isAirborne) {
      Jump();
      animator.SetTrigger("jump");
    } else if (availableHops > 0) {
      Jump();
      availableHops--;
      animator.SetTrigger("hop");
    }
  }

  void Jump () {
    jumpPerformedTimestamp = Time.time;
    body.velocity = Utilz.SetY(body.velocity, Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight));
    animator.SetBool("is airborne", true);
  }
}

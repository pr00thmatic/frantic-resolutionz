using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Airbornez : MonoBehaviour {
  [Header("Configuration")]
  public float jumpHeight;
  public InputAction jumpControlz;

  [Header("Information")]
  public bool isAirborne = false;
  public RaycastHit hit;
  public float Radius { get => collider.radius; }
  public float jumpPerformedTimestamp = 0;

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
    animator.SetBool("is airborne", isAirborne || Time.time - jumpPerformedTimestamp < 0.25f);
  }

  public void OnJumpRequested (InputAction.CallbackContext ctx) {
    if (!isAirborne) {
      jumpPerformedTimestamp = Time.time;
      body.AddForce(Mathf.Sqrt(-2 * Physics.gravity.y * jumpHeight) * Vector3.up, ForceMode.VelocityChange);
      animator.SetBool("is airborne", true);
      animator.SetTrigger("jump");
    }
  }
}

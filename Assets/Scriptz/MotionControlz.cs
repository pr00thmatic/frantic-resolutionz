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

  [Header("Initialization")]
  public Animator animatorz;
  public Animator rootAnimatorz;

  void OnEnable () {
    horizontalControlz.Enable();
    rootAnimatorz.SetInteger("orientation", orientation);
    animatorz.SetTrigger("turn");
  }

  void Update () {
    desiredHorizontal = horizontalControlz.ReadValue<float>();
    animatorz.SetFloat("speed x", Mathf.Abs(desiredHorizontal));
    body.velocity = Utilz.SetX(body.velocity, desiredHorizontal * maxSpeed);

    if (desiredHorizontal != 0 && Mathf.Sign(desiredHorizontal) != orientation) {
      orientation = (int) Mathf.Sign(desiredHorizontal);
      rootAnimatorz.SetInteger("orientation", orientation);
      animatorz.SetTrigger("turn");
    }
  }
}

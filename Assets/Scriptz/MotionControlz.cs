using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class MotionControlz : MonoBehaviour {
  [Header("Configuration")]
  public float maxSpeed = 5;
  public InputAction horizontalControlz;

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

    if (desiredHorizontal != 0 && Mathf.Sign(desiredHorizontal) != orientation) {
      orientation = (int) Mathf.Sign(desiredHorizontal);
      rootAnimatorz.SetInteger("orientation", orientation);
      animatorz.SetTrigger("turn");
    }

    transform.position += Vector3.right * desiredHorizontal * maxSpeed * Time.deltaTime;
  }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackingCameraz : MonoBehaviour {
  [Header("Configuration")]
  public float speed = 180;

  [Header("Information")]
  public int targetOrientation = 0;
  public Vector3 targetEulerRotation;

  [Header("Initialization")]
  public MotionControlz birdz;

  void OnEnable () {
    targetEulerRotation = transform.rotation.eulerAngles;
  }

  void Update () {
    targetOrientation = birdz.desiredHorizontal == 0? 0: birdz.orientation;
    targetEulerRotation.y = targetOrientation == 0? 0: targetOrientation < 0? -9: 9;
    transform.rotation =
      Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetEulerRotation), Time.deltaTime * speed);
  }
}

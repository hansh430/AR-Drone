using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchInPinchOut : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private float initialDistance;
    private Vector3 initialScale;
    private Vector2 lastTouchPosition;
    private void Update()
    {
        ZoomInZoomout();
        RotateGameObject();
    }
    private void ZoomInZoomout()
    {
        if (Input.touchCount == 2)
        {
            Touch touch_0 = Input.GetTouch(0);
            Touch touch_1 = Input.GetTouch(1);
            if (touch_0.phase == TouchPhase.Began || touch_1.phase == TouchPhase.Began)
            {
                initialDistance = Vector3.Distance(touch_0.position, touch_1.position);
                initialScale = transform.localScale;
            }
            else if (touch_0.phase == TouchPhase.Moved || touch_1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector3.Distance(touch_0.position, touch_1.position);
                float scaleFactor = currentDistance / initialDistance;
                transform.localScale = initialScale * scaleFactor;
            }
        }
    }
   private void RotateGameObject()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    Vector2 swipeDelta = touch.position - lastTouchPosition;
                    float rotationAmount = -swipeDelta.x * rotationSpeed * Time.deltaTime;
                    transform.Rotate(Vector3.up, rotationAmount, Space.World);
                    lastTouchPosition = touch.position;
                    break;
            }
        }
    }
}

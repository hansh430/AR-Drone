using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] private float speed;
    private FixedJoystick fixedJoystick;
    private Rigidbody rigidBody;
    private float xVal, yVal;
    private void OnEnable()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        xVal = fixedJoystick.Horizontal;
        yVal = fixedJoystick.Vertical;
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(xVal, 0, yVal);
        rigidBody.velocity = movement;
        if (xVal != 0 && yVal != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(xVal,yVal)*Mathf.Rad2Deg, transform.eulerAngles.z);
        }
    }
}

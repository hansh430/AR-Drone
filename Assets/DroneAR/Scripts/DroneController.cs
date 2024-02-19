using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneController : MonoBehaviour
{
    enum DroneState
    {
        DRONE_STATE_IDLE,
        DRONE_STATE_START_TAKINGOFF,
        DRONE_STATE_TAKINGOFF,
        DRONE_STATE_MOVING_UP,
        DRONE_STATE_FLYING,
        DRONE_STATE_START_LANDING,
        DRONE_STATE_LANDING,
        DRONE_STATE_LANDED,
        DRONE_STATE_WAIT_ENGINE_STOP
    }
    public static DroneController instance;
    DroneState state;
    private Animator _animator;
    private Vector3 _Speed = new Vector3(0f, 0f, 0f);
    [SerializeField] private float _moveSpeed = 5f;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        state = DroneState.DRONE_STATE_IDLE;
    }
    public bool IsIdle()
    {
        return (state == DroneState.DRONE_STATE_IDLE);
    }
    public void TakeOff()
    {
        state = DroneState.DRONE_STATE_START_TAKINGOFF;
    }
    public bool IsFlying()
    {
        return (state == DroneState.DRONE_STATE_FLYING);
    }
    public void Land()
    {
        state = DroneState.DRONE_STATE_START_LANDING;
    }
    public void Move(float speedX, float speedZ)
    {
        _Speed.x = speedX;
        _Speed.z = speedZ;
        UpdateDrone();
    }
    private void UpdateDrone()
    {
        switch (state)
        {
            case DroneState.DRONE_STATE_IDLE:
                break;

            case DroneState.DRONE_STATE_START_TAKINGOFF:
                _animator.SetBool("TakeOff", true);
                state = DroneState.DRONE_STATE_TAKINGOFF;
                break;

            case DroneState.DRONE_STATE_TAKINGOFF:
                if (_animator.GetBool("TakeOff") == false)
                {
                    state = DroneState.DRONE_STATE_MOVING_UP;
                }
                break;
            case DroneState.DRONE_STATE_MOVING_UP:
                if (_animator.GetBool("MoveUp") == false)
                {
                    state = DroneState.DRONE_STATE_FLYING;
                }
                break;
            case DroneState.DRONE_STATE_FLYING:
                float angleZ = -30.0f * _Speed.x;
                float angleX = 30f * _Speed.z;
                Vector3 rotation = transform.localRotation.eulerAngles;
                transform.localPosition += _Speed * _moveSpeed * Time.deltaTime;
                transform.localRotation = Quaternion.Euler(angleX, rotation.y, angleZ);
                break;
            case DroneState.DRONE_STATE_START_LANDING:
                _animator.SetBool("MoveDown", true);
                state = DroneState.DRONE_STATE_LANDING;
                break;

            case DroneState.DRONE_STATE_LANDING:
                if (_animator.GetBool("MoveDown") == false)
                {
                    state = DroneState.DRONE_STATE_LANDED;
                }
                break;
            case DroneState.DRONE_STATE_LANDED:
                _animator.SetBool("Land", true);
                state = DroneState.DRONE_STATE_WAIT_ENGINE_STOP;
                break;
            case DroneState.DRONE_STATE_WAIT_ENGINE_STOP:
                if (_animator.GetBool("Land") == false)
                {
                    state = DroneState.DRONE_STATE_IDLE;
                }
                break;
        } 
    }

}

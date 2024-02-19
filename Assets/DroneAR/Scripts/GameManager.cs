using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button flyButton;
    [SerializeField] private Button landButton;
    [SerializeField] private GameObject controlButtons;
    struct DroneAnimationControls
    {
        public bool _moving;
        public bool _interpolatingAsc;
        public bool _interpolatingDesc;
        public float _axis;
        public float _direction;
    }
    DroneAnimationControls _MovingLeft;
    DroneAnimationControls _MovingBack;
    private void Start()
    {
        flyButton.onClick.AddListener(EventOnClickFlyButton);
        landButton.onClick.AddListener(EventOnClickLandButton);
    }
    private void Update()
    {

        UpdateControls(ref _MovingLeft);
        UpdateControls(ref _MovingBack);
        if (DroneController.instance != null)
        {
            DroneController.instance.Move(_MovingLeft._axis * _MovingLeft._direction, _MovingBack._axis * _MovingBack._direction);
        }
    }

    private void UpdateControls(ref DroneAnimationControls _controls)
    {
        if (_controls._moving || _controls._interpolatingAsc || _controls._interpolatingDesc)
        {
            if (_controls._interpolatingAsc)
            {
                _controls._axis += 0.05f;
                if (_controls._axis >= 1f)
                {
                    _controls._axis = 1f;
                    _controls._interpolatingAsc = false;
                    _controls._interpolatingDesc = true;
                }
            }
            else if (!_controls._moving)
            {
                _controls._axis -= 0.05f;
                if (_controls._axis <= 0f)
                {
                    _controls._axis = 0f;
                    _controls._interpolatingDesc = false;
                }
            }
        }
    }
    private void EventOnClickFlyButton()
    {
        if (DroneController.instance.IsIdle())
        {
            DroneController.instance.TakeOff();
            flyButton.gameObject.SetActive(false);
            landButton.gameObject.SetActive(true);
            controlButtons.SetActive(true);
        }
    }
    private void EventOnClickLandButton()
    {
        if (DroneController.instance.IsFlying())
        {
            DroneController.instance.Land();
            landButton.gameObject.SetActive(false);
            flyButton.gameObject.SetActive(true);
            controlButtons.SetActive(false);
        }
    }


    public void EventOnPressedLeftButton()
    {
        _MovingLeft._moving = true;
        _MovingLeft._interpolatingAsc = true;
        _MovingLeft._direction = -1.0f;
    }
    public void EventOnReleasedLeftButton()
    {
        _MovingLeft._moving = false;
    }

    public void EventOnPressedRightButton()
    {
        _MovingLeft._moving = true;
        _MovingLeft._interpolatingAsc = true;
        _MovingLeft._direction = 1.0f;
    }
    public void EventOnReleasedRightButton()
    {
        _MovingLeft._moving = false;
    }
    public void EventOnPressedBackButton()
    {
        _MovingBack._moving = true;
        _MovingBack._interpolatingAsc = true;
        _MovingBack._direction = -1.0f;
    }
    public void EventOnReleasedBackButton()
    {
        _MovingBack._moving = false;
    }
    public void EventOnPressedForwardButton()
    {
        _MovingBack._moving = true;
        _MovingBack._interpolatingAsc = true;
        _MovingBack._direction = 1.0f;
    }
    public void EventOnReleasedForwardButton()
    {
        _MovingBack._moving = false;
    }

}

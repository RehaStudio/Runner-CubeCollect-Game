using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class PlayerController : MonoBehaviour
{
    [Header("Movement")] public float currentSpeed = 0;

    [SerializeField] float swipeSpeed = 5;
    [SerializeField] float maxXPosition = 2.5f;
    [SerializeField] float minXPosition = -2.5f;

    public Rigidbody _rigidbody;
    private float _lastXPosition;
    private Vector3 _firstTouchPosition;
    private Vector3 _lastTouchPosition;
    private Vector3 _touchDirection;
    private Vector3 _lastPlayerPosition;
    private Vector3 _playerVelocity;
    private Transform _playerTransform;
    Camera _camera;
    bool isMoving;
    protected void Start()
    {
        if (!(Camera.main is null)) 
            _camera = Camera.main;
        _playerTransform = transform;
        EventManager.levelStarted += EventManagerLevelStarted;
    }

    private void EventManagerLevelStarted()
    {
        isMoving = true;
    }

    protected void Update()
    {
        InputControl();
        if (isMoving)
        {
            SlideMovement();
        }
    }
    protected void FixedUpdate()
    {
        if (isMoving)
        {
            CharacterMovement();
        }
    }

    private void CharacterMovement()
    {
        Vector3 velocity = new Vector3(0, 0, currentSpeed);
        _rigidbody.velocity = velocity;
        Vector3 tar = transform.localPosition +
                                Vector3.right * _playerVelocity.x * swipeSpeed * Time.deltaTime;
        transform.localPosition = tar.VectorXZ() + transform.localPosition.y * Vector3.up;
    }
    private void SlideMovement()
    {
        if (Input.GetMouseButton(0))
        {
            var directionX = _lastXPosition + (_touchDirection.x);
            directionX = Mathf.Clamp(directionX, minXPosition, maxXPosition);
            directionX -= _playerTransform.localPosition.x;
            _playerVelocity.x = directionX;
        }
        else
        {
            _playerVelocity.x = 0;
        }
    }

    private void InputControl()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _firstTouchPosition = GetInputPosition();
            }
            _lastTouchPosition = GetInputPosition();
            _touchDirection = (_lastTouchPosition - _firstTouchPosition) * swipeSpeed;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _firstTouchPosition = Vector3.zero;
            _lastTouchPosition = Vector3.zero;
            _touchDirection = Vector3.zero;
            _lastXPosition = transform.localPosition.x;
            _lastPlayerPosition = transform.localPosition;
        }
    }
    private Vector3 GetInputPosition()
    {
        Vector3 inputPosition = Input.mousePosition;
        var cameraPosition = _camera.transform.position;
        inputPosition.z = _playerTransform.localPosition.z - cameraPosition.z;
        inputPosition.x = inputPosition.x + cameraPosition.x;
        Vector3 worldPoint = _camera.ScreenToViewportPoint(inputPosition) - Vector3.one * 0.5f;
        return worldPoint;
    }
    protected void StopMove()
    {
        isMoving = false;
        _rigidbody.velocity = Vector3.zero;
    }
    private void OnDisable()
    {
        EventManager.levelStarted -= EventManagerLevelStarted;
    }
    protected bool IsMoving => isMoving;
}
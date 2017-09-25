

using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private Transform _targetMoveTransform;
    public Transform TargetTransformRotation;
    
    
    [SerializeField]
    private float _verticalVelocity = 4.0f;

    [SerializeField]
    private float _turnSpeed = 4.0f;
    public float WalkSpeed = 1.0f;
    public float RunSpeed = 5.0f;
    public float WalkBackSpeed = -1.0f;
    public float WalkRightSpeed = 1.0f;
    public float WalkLeftSpeed = -1.0f;
    
    private Animator _animator;
    private int _currentState;
    private int _stateNameHash;
    
    private float _vertical;
    private float _horizontal;

    private Locomotion _locomotion;

    void Start ()
    {
        _stateNameHash = Animator.StringToHash("Estado");
        _animator = GetComponentInChildren<Animator>();
        _currentState = 0;
        _horizontal = transform.eulerAngles.y;
        _vertical = transform.eulerAngles.x;
        _locomotion = new Locomotion(_animator);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdateTick()
    {
        UpdateRotation();
        Controle();

        Move();
        UpdateAnimatorState();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void UpdateRotation()
    {
        var mouseVertical = Input.GetAxis("Mouse Y");
        _vertical = (_vertical - _verticalVelocity * mouseVertical) % 360f;
        _vertical = Mathf.Clamp(_vertical, -30, 60);
        TargetTransformRotation.localRotation = Quaternion.AngleAxis(_vertical, Vector3.right);
    }

    private void UpdateAnimatorState()
    {
        //_animator.SetInteger(_stateNameHash, _currentState);
    }

    private void Controle()
    {
        /*
        Estado:
        01 = Walking
        02 = Running
        03 = Walking Back
        04 = Walking Right
        05 = Walking Left
        */

        if (Input.GetKeyDown("w"))
        {
            _currentState = 1;
        }
        if (Input.GetKeyUp("w") && _currentState == 1)
        {
            _currentState = 0;
            if (Input.GetKey("s")) { _currentState = 3; }
            if (Input.GetKey("a")) { _currentState = 5; }
            if (Input.GetKey("d")) { _currentState = 4; }
        }
        if (Input.GetKeyUp("w") && _currentState == 2)
        {
            _currentState = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _currentState == 1)
        {
            _currentState = 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && _currentState == 2) { _currentState = 1; }
                
        if (Input.GetKeyDown("s"))
        {
            _currentState = 3;
        }
        if (Input.GetKeyUp("s") && _currentState == 3)
        {
            _currentState = 0;
            if (Input.GetKey("a")) { _currentState = 5; }
            if (Input.GetKey("d")) { _currentState = 4; }
            if (Input.GetKey("w")) { _currentState = 1; }
        }

        if (Input.GetKeyDown("d"))
        {
            _currentState = 4;
        }
        if (Input.GetKeyUp("d") && _currentState == 4)
        {
            _currentState = 0;
            if (Input.GetKey("s")) { _currentState = 3; }
            if (Input.GetKey("a")) { _currentState = 5; }
            if (Input.GetKey("w")) { _currentState = 1; }

        }

        if (Input.GetKeyDown("a"))
        {
            _currentState = 5;
        }
        if (Input.GetKeyUp("a") && _currentState == 5)
        {
            _currentState = 0;
            if (Input.GetKey("s")) { _currentState = 3; }
            if (Input.GetKey("d")) { _currentState = 4; }
            if (Input.GetKey("w")) { _currentState = 1; }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (_currentState == 2)
            {
                _currentState = 1;
            }
        }
    }
   
    private void Move()
    {
        var mouseHorizontal = Input.GetAxis("Mouse X");
        _horizontal = (_horizontal + _turnSpeed * mouseHorizontal) % 360f;
        _targetMoveTransform.rotation = Quaternion.AngleAxis(_horizontal, Vector3.up);

        if (_currentState == 0)
        {
            _targetMoveTransform.Translate(0, 0, 0);
            _locomotion.Do(0, 0);
        }
        if (_currentState == 1)
        {
            _targetMoveTransform.Translate(0, 0, WalkSpeed * Time.deltaTime);
            _locomotion.Do(0, 0);
        }
        if (_currentState == 2)
        {
            _targetMoveTransform.Translate(0, 0, RunSpeed * Time.deltaTime);
            _locomotion.Do(1000, 0);
        }
        if (_currentState == 3)
        {
            _targetMoveTransform.Translate(0, 0, WalkBackSpeed * Time.deltaTime);
            _locomotion.Do(-1000, 0);
        }
        if (_currentState == 4)
        {
            _targetMoveTransform.Translate(WalkRightSpeed * Time.deltaTime, 0, 0);
            _locomotion.Do(1000, 90);
        }
        if (_currentState == 5)
        {
            _targetMoveTransform.Translate(WalkLeftSpeed * Time.deltaTime, 0, 0);
            _locomotion.Do(1000, -90);
        }
    }
}

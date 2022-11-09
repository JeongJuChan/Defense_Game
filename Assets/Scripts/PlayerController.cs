using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public enum AttackType
{
    None,
    Sword,
    Dagger
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviourPunCallbacks
{
    //무기 장착 여부 
    public int _damage = 5;
    
    [SerializeField] float _speed = 100f;
    [SerializeField] float _radius = 1f;
    [SerializeField] float _jumpPower = 0.4f;
    [SerializeField] float _sprintSpeed = 200f;
    [SerializeField] bool _isGrounded;
    [SerializeField] bool _isSprint;
    [SerializeField] Transform _camTrans;
    [SerializeField] float _moveAnimationMod = 200f;
    [SerializeField] float _jumpTime = 1f;
    [SerializeField] float _jumpTimer = 0f;
    [SerializeField] AttackType _attackType;
    [SerializeField] bool _isEquip;

    int _animXSpeed;
    int _animYSpeed;
    int _animJump;
    int _animAttack;
    int _animIsEquip;

    float _offsetSpeed;
    bool _canJump = true;
    Vector3 _direction;
    Rigidbody _rigidbody;
    Vector3 _moveDir;
    Animator _animator;
    AnimationEventHandler _animationEventHandler;
    Transform _playerBody;
    PhotonView _pv;
    Inventory _inventory;
    Item _currentItem;
    bool _itemActive;

    
    
    void Start()
    {
        Debug.Log("플레이어 생성 !");
        Init();
        AnimInit();
    }
    
    void Init()
    {
        _pv = GetComponent<PhotonView>();
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _offsetSpeed = _speed;
        _speed = 0;
        _playerBody = _animator.transform;
        _animationEventHandler = GetComponentInChildren<AnimationEventHandler>();
        _inventory = GetComponent<Inventory>();
        _animationEventHandler.AttackEvent += AttackEvent;
        _animationEventHandler.EquipEvent += Equip;
    }

    void AnimInit()
    {
        _animXSpeed = Animator.StringToHash("xSpeed");
        _animYSpeed = Animator.StringToHash("ySpeed");
        _animAttack = Animator.StringToHash("AttackType");
        _animJump = Animator.StringToHash("isJump");
        _animIsEquip = Animator.StringToHash("isEquip");
    }

    void Update()
    {
        if (!_pv.IsMine)
            return;
        JumpDelayTimer();
    }

    void FixedUpdate()
    {
        if (!_pv.IsMine)
            return;
        Move();
        CheckGround();
    }

    void OnMove(InputValue value)
    {
        Vector2 inputMove = value.Get<Vector2>();
        _direction.x = inputMove.x;
        _direction.z = inputMove.y;
        _speed = Mathf.Approximately(_direction.magnitude, 0f) ? 0f : _offsetSpeed;
    }

    void OnJump()
    {
        if (!_isGrounded || !_canJump)
            return;
        Vector3 forceVel = _rigidbody.velocity;
        forceVel.y = -_jumpPower * Physics.gravity.y;
        _rigidbody.velocity = forceVel;
        _animator.SetBool(_animJump, true);
        _jumpTimer = _jumpTime;
    }

    void OnSprint(InputValue value)
    {
        _isSprint = value.isPressed;
    }


    // 공격 (Input Actions)
    void OnFire()
    {
        //내 플레이어만 수행하도록 IsMine 처리 
        if (!_pv.IsMine)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        //장착 아이템별로 공격 애니메이션 상이 처리 
        if (!_isEquip)
            _attackType = (AttackType)Random.Range(-2, 0);
        
        _animator.SetInteger(_animAttack, (int)_attackType);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_playerBody.position, _radius);
        Gizmos.DrawRay(_playerBody.position + Vector3.up * 0.9f, _playerBody.forward);
    }

    void JumpDelayTimer()
    {
        
        if (_jumpTimer <= 0.0f)
        {
            _canJump = true;
            _animator.SetBool(_animJump, false);
        }

        if (_jumpTimer > 0.0f)
        {
            _jumpTimer -= Time.deltaTime;
            _canJump = false;
        }
    }

    void Move()
    {
        
        float targetSpeed = _isSprint ? _sprintSpeed : _speed;
        float fallSpeed = _rigidbody.velocity.y;
        if (_direction.sqrMagnitude != 0)
        {
            Vector3 lookForward = new Vector3(_camTrans.forward.x, 0f, _camTrans.forward.z).normalized;
            Vector3 lookRight = new Vector3(_camTrans.right.x, 0f, _camTrans.right.z).normalized;
            //_moveDir = transform.TransformDirection(_direction).normalized * (targetSpeed * Time.deltaTime);
            _moveDir = (_direction.z * lookForward + _direction.x * lookRight) * (targetSpeed * Time.deltaTime);
            // 방향 일치시켜주기 - y를 안 넣는 이유는 앞으로 기울어지기 때문
            // transform.forward = lookForward로 하면 카메라에 따라 캐릭터도 회전하게 됨
            _playerBody.forward = _moveDir;
            _moveDir.y = fallSpeed;
            _rigidbody.velocity = _moveDir;
        }

        // Vector3 heading = _camera.localRotation * _direction;
        // transform.rotation = Quaternion.LookRotation(_direction);

        MoveAnimation(targetSpeed);
    }

    void MoveAnimation(float targetSpeed)
    {
        // 애니메이션
        float animSpeedY = _direction.z * targetSpeed / _moveAnimationMod;
        float animSpeedX = _direction.x * _speed / _moveAnimationMod;
        _animator.SetFloat(_animYSpeed, animSpeedY);
        _animator.SetFloat(_animXSpeed, animSpeedX);
    }

    void CheckGround()
    {
        if (Physics.CheckSphere(transform.position, _radius, 1 << 6, QueryTriggerInteraction.Ignore))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    public void AttackEvent(int damage)
    {
        RaycastHit hit;
        Debug.DrawRay(_playerBody.position + Vector3.up * 1.5f, _playerBody.forward * 2);
        
        _animator.SetInteger(_animAttack, (int)AttackType.None);
        
        if (!Physics.Raycast(_playerBody.position + Vector3.up * 0.9f, _playerBody.forward, out hit, 1f))
            return;

        // 플레이어 데미지 처리방식 (IDamageble : 인터페이스 )
        Debug.Log("Player Attack!");
        IDamageable damageable = hit.collider.GetComponent<IDamageable>();
        if (damageable.Equals(null))
            return;
        Debug.Log(damageable);
        Debug.Log(damage);
        damageable.Damage(damage);
    }

    public void SetAttackType(AttackType attackType)
    {
        _attackType = attackType;
    }

    public AttackType GetAttackType()
    {
        return _attackType;
    }

    public bool GetIsEquip()
    {
        return _isEquip;
    }

    public void SetIsEquip(bool isEquip)
    {
        _isEquip = isEquip;
        _animator.SetBool(_animIsEquip, isEquip);
    }

    public void WeaponSet(Item item, bool isActive)
    {
        _currentItem = item;
        _itemActive = isActive;
    }

    void Equip()
    {
        _inventory.ItemDict[_currentItem.ItemList].SetActive(_itemActive);
    }

    
}


using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform _camTrans;
    [SerializeField] float _deltaMod = 0.25f; 
    
    float _xRotation;
    float _yRotation;
    PhotonView _pv;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _pv = GetComponent<PhotonView>();
        if (_pv.IsMine && !_camTrans.gameObject.activeSelf)
            _camTrans.gameObject.SetActive(true);
    }

    void LateUpdate()
    {
        if (!_pv.IsMine)
            return;
        Follow();
    }

    void Follow()
    {
        _camTrans.position = transform.position;
    }

    void OnLook(InputValue value)
    {
        Vector2 mouseDelta = value.Get<Vector2>();
        _yRotation += mouseDelta.x * _deltaMod;

        _xRotation -= mouseDelta.y;
        float xClampRotation = Mathf.Clamp(_xRotation * _deltaMod, -30f, 40f);
        _camTrans.localRotation = Quaternion.Euler(xClampRotation, _yRotation, 0f);
    }
    
   
}

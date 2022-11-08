using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform _camTrans;
    [SerializeField] float deltaMod = 0.25f; 
    float _xRotation;
    float yRotation;
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
        yRotation += mouseDelta.x * deltaMod;

        _xRotation -= mouseDelta.y;
        float xClampRotation = Mathf.Clamp(_xRotation * deltaMod, -30f, 40f);
        _camTrans.localRotation = Quaternion.Euler(xClampRotation, yRotation, 0f);
        
        /*Vector3 camAngle = camTrans.rotation.eulerAngles;
        float ClampAngleX = camAngle.x - mouseDelta.y;
        ClampAngleX = ClampAngleX < 180 ? Mathf.Clamp(ClampAngleX, 0f, 40f) : Mathf.Clamp(ClampAngleX, 330f, 360f);
        camTrans.rotation = Quaternion.Euler(ClampAngleX, camAngle.y + mouseDelta.x, camAngle.z);*/
    }
    
   
}

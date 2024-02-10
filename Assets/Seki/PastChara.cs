using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PastChara : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private float _horizontal;
    private float _vertical;
    private Vector3 _velocity;
    private float _speed = 2f;
    [SerializeField]
    private ThirdStageGM thirdGM;
    Vector2 inputMoveVelocity = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        //playerSpeed = thirdGM.PlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        inputMoveVelocity.x = Input.GetAxis("Horizontal");
        _velocity = new Vector3(inputMoveVelocity.x*_speed, _rigidbody.velocity.y, _speed).normalized;
        _rigidbody.velocity = _velocity * _speed;

    }

    private void FixedUpdate() {
        //rb.velocity = new Vector3(movingVelocity.x, rb.velocity.y, movingVelocity.z);
    }
}

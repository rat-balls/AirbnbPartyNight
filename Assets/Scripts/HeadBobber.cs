using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    [SerializeField, Range(0,0.1f)] private float _amplitudeY = 0.1f;
    [SerializeField, Range(0,0.1f)] private float _amplitudeX = 0.005f;
    [SerializeField, Range(0,30f)] private float _frequency = 10.5f;
    
    [SerializeField] private Transform _camera = null;

    private float _toggleSpeed = 2.0f;
    private Vector3 _startPos;
    private CharacterController _controller;
    
    private void Awake(){
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
    }

    void Update(){
        CheckMotion();
    }


    private void CheckMotion(){
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;

        ResetPosition();
        if (speed < _toggleSpeed) return;
        if (!_controller.isGrounded) return;

        PlayMotion(FootstepMotion());
    }

    private Vector3 FootstepMotion(){
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitudeY;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitudeX * 2;
        return pos;
    }

    private void ResetPosition(){
        if(_camera.localPosition == _startPos) return;
        if(_controller.height < 1.6) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private void PlayMotion(Vector3 motion){
    _camera.localPosition += motion; 
    }
}
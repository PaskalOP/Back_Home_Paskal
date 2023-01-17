using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackHome
{
    public class CameraController : MonoBehaviour
    {
        private LevelObjectView _playerView;
        private Transform _playerTransform;
        private Transform _cameraTransform;

        private float _cameraSpeed = 1.2f;
        private float X;
        private float Y;

        private float _offsetX;
        private float _offsetY;

        private float _xAxisInput;
        private float _yAxisInput;

        private float _treshold;

        public  CameraController(LevelObjectView playerView, Transform cameraTransform)
        {
            _treshold = 0.2f;
            _playerView = playerView;
            _playerTransform = playerView._transform;
            _cameraTransform = cameraTransform;

        }

        public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisInput = _playerView._rb.velocity.y;
            X = _playerTransform.position.x;
            Y = _playerTransform.position.y;

            if (_xAxisInput > _treshold)
            {
                _offsetX = 4;
            }
            else if (_xAxisInput <-_treshold)
            {
                _offsetX = -4;
            }
            else
            {
                _offsetX = 0;
            }

            if (_yAxisInput > _treshold)
            {
                _offsetY = 4;
            }
            else if (_yAxisInput < -_treshold)
            {
                _offsetY = -4;
            }
            else
            {
                _offsetY = 0;
            }

            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, new Vector3(X + _offsetX, Y + _offsetY, _cameraTransform.position.z), Time.deltaTime * _cameraSpeed);
        }
    }
}


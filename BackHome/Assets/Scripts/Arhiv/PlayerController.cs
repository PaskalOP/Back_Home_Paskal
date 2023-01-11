using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackHome
{
    public class PlayerController 
    {
        private AnimationConfig _config;
        private SpriteAnimatorControler _playerAnimator;
        private LevelObjectView _playerView;

        
        private bool _isJump;
        private bool _isMoving;

        private float _xAxisInput; 
        private float _speedWalk = 3f;
        private float _animationSpeed = 10f;
        private float _movingTreshold = 0.1f; //погрешность при движении
        private float _jumpForce = 7f;
        private float _jumpTreshold = 1f; //погрешность при прыжке
        private float _g = -9.8f; //коеффициент ускорения свободного падения
        private float _groundLevel = 0.5f; //уровень земли
        private float _yVelocity; //ускорение свободного падения

        private Vector3 _leftScale = new Vector3(-1, 1, 1); //поворот в лево
        private Vector3 _rightScale = new Vector3(1, 1, 1); //поворот в право

        private Transform _playerT;


        public PlayerController(LevelObjectView player) 
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimatorControler(_config);
            _playerAnimator.StartAnimation(player._spriteRenderer, AnimState.Run, true, _animationSpeed);
            _playerView = player;
            _playerT = player._transform; 
        }

        private void MoveTovards()//метод двидения и поворота спрайта 
        {
            _playerT.position += Vector3.right * Time.deltaTime * _speedWalk * (_xAxisInput < 0 ? -1 : 1);
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;  

        }

        public bool IsGrounded() //метод определяющий стоим ли мы на земле или нет
        {
            return _playerT.position.y <= _groundLevel && _yVelocity <= 0;
        }

        public  void Update()
        {
            _playerAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;

            //дальше пишем стейт машинку

            if (_isMoving)
                MoveTovards();

            if(IsGrounded())
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if (_isJump && _yVelocity <= 0)
                    _yVelocity = _jumpForce;
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _playerT.position = new Vector3(_playerT.position.x, _groundLevel, _playerT.position.z); 
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpTreshold)
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, true, _animationSpeed);

                _yVelocity += _g * Time.deltaTime;
                _playerT.position += Vector3.up * (Time.deltaTime * _yVelocity); 
            }

        }
    }
}


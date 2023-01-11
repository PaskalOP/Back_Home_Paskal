using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackHome
{
    public class PlayerContrPhys
    {
        private AnimationConfig _config;
        private SpriteAnimatorControler _playerAnimator;
        private InteractiveLevelObjectView _playerView;
        private Rigidbody2D _rb;
        private ContactPooler _contactPooller;


        private bool _isJump;
        private bool _isMoving;

        private float _xAxisInput;
        private float _speedWalk = 150f;
        private float _animationSpeed = 10f;
        private float _movingTreshold = 0.1f; //погрешность при движении
        private float _jumpForce = 8f;
        private float _jumpTreshold = 1f; //погрешность при прыжке
        private float _yVelocity; //ускорение свободного падения
        private float _xVelocity = 0;
        private float _health=100;

        private Vector3 _leftScale = new Vector3(-1, 1, 1); //поворот в лево
        private Vector3 _rightScale = new Vector3(1, 1, 1); //поворот в право

        private Transform _playerT;


        public PlayerContrPhys(InteractiveLevelObjectView player)
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimatorControler(_config);
            _playerAnimator.StartAnimation(player._spriteRenderer, AnimState.Run, true, _animationSpeed);
            _playerView = player;
            _playerT = player._transform;
            _rb = player._rb;
            _contactPooller = new ContactPooler(_playerView._collider);
            
            player.TakeDamage += TakeBullet;
        }

        private void MoveTovards()//метод двидения и поворота спрайта 
        {
            _xVelocity = Time.fixedDeltaTime * _speedWalk * (_xAxisInput < 0 ? -1 : 1);
            _rb.velocity = new Vector2(_xVelocity, _yVelocity); 
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;

        }

        public  void TakeBullet(BulletView bullet)
        {
            _health -= bullet.Damage; 
        }
        public void Update()
        {
            if (_health <= 0)
            {
                _playerView._spriteRenderer.enabled = false;  
            }

            _playerAnimator.Update();
            _contactPooller.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;
            _yVelocity = _rb.velocity.y;  

            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;

            

            if (_isMoving)
            {
                MoveTovards();
                
            }
            else
            {
                _xVelocity = 0;
                _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            }


            if (_contactPooller.IsGrounded)
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);
                if (_isJump && _yVelocity <= _jumpTreshold)
                {
                    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
                    
               
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpTreshold)
                {
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, true, _animationSpeed);
                }
                    
            }

        }
    }
}


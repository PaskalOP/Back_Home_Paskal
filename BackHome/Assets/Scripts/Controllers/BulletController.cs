using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackHome
{
    public class BulletController 
    {
        private BulletView _bulletView;
        private Vector3 _velocity; 

        public BulletController(BulletView bulletView)
        {
            _bulletView = bulletView;
            Active(false);
        }

        public void Active(bool val)
        {
            _bulletView.gameObject.SetActive(val);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;

            float  _angle = Vector3.Angle(Vector3.left, _velocity);
            Vector3 _axis = Vector3.Cross(Vector3.down, _velocity);

            _bulletView.transform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }

        public void Trow(Vector3 position, Vector3 velocity)
        {
            _bulletView.transform.position = position;
            SetVelocity(_velocity);
            _bulletView._rb.velocity = Vector2.zero;
            _bulletView._rb.angularVelocity = 0;
            Active(true);
            _bulletView._rb.AddForce(velocity, ForceMode2D.Impulse);

        }
    }
}


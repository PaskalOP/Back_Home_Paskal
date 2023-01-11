using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackHome
{
    public class EmitterController 
    {
        private List<BulletController> _bulletControllers = new List<BulletController> ();
        private Transform _transform;

        private int _index;
        private float _time; //время до следующей пули
        private float _startSpeed = 15; //стартовая скорость
        private float _delay=1; //задержка для отсчета


        public EmitterController(List<BulletView> bulletViews, Transform emitterTransform)
        {
            _transform = emitterTransform;
            foreach (BulletView bulletview in bulletViews)
            {
                _bulletControllers.Add(new BulletController(bulletview));
            }
        }
        public  void Update()
        {
            if (_time > 0)
            {
                _bulletControllers[_index].Active(false);
                _time -= Time.deltaTime;  
            }
            else
            {
                _time = _delay;
                _bulletControllers[_index].Trow(_transform.position, -_transform.up * _startSpeed);
                _index++;

                if (_index>= _bulletControllers.Count)
                {
                    _index = 0;
                }
            }
        }
    }
}


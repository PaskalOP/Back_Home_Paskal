using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControlier 
{
    private Transform _mazzleT;
    private Transform _target;

    private Vector3 _direction;
    private Vector3 _axis; //ось поворота
    private float _angle; // угол поворота


    public CannonControlier(Transform mazzleT, Transform target)
    {
        _mazzleT = mazzleT;
        _target = target;
    }
    public void Update()
    {
        _direction = _target.position - _mazzleT.position;
        _angle = Vector3.Angle(Vector3.down, _direction);
        _axis = Vector3.Cross(Vector3.down, _direction);

        _mazzleT.rotation = Quaternion.AngleAxis(_angle, _axis); 
    }
}

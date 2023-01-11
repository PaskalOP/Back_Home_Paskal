using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackHome
{
    public class BulletView : LevelObjectView
    {
        private int _damage = 10;
        public int Damage { get => _damage; set => _damage = value; }
    }
}
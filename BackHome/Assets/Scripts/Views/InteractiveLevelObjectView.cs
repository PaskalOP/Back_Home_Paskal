using System;
using UnityEngine;

namespace BackHome
{
    public class InteractiveLevelObjectView : LevelObjectView 
    {
       
        public Action <BulletView > TakeDamage { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent (out BulletView contactView))
            {
                TakeDamage?.Invoke(contactView);

            }
        }
    }
}


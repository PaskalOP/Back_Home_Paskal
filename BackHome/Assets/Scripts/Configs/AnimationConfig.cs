using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackHome
{

    public enum AnimState // перечесление нужных анимаций
    {
        Idle = 0,
        Run = 1,
        Jump = 2
    }

    [CreateAssetMenu(fileName = "SpriteAnimCfg", menuName = "Configs/Animation", order = 1)]
    //Создаем пункт меню, который будет вызывать анимационный конфиг

    public class AnimationConfig : ScriptableObject
    {
        [Serializable]
        public class SpiteSequence
        {
            public AnimState Trak;
            public List<Sprite> Sprites = new List<Sprite>();
        }
        public List<SpiteSequence> Sequences = new List<SpiteSequence>();

    }


}
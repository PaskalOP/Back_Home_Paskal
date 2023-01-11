using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackHome
{
    public class SpriteAnimatorControler : IDisposable
    {
        private sealed class Animation //вложенный класс с параметрами анимации
        {
            public List<Sprite> Sprites;
            public AnimState Track;
            public bool Loop;
            public bool Sleep;
            public float Speed=10;
            public float counter = 0;

            public void Update()
            {
                if (Sleep) return;
                counter += Time.deltaTime * Speed;

                if (Loop)
                {
                    while (counter > Sprites.Count)
                    {
                        counter -= Sprites.Count;
                    }
                }
                else if (counter > Sprites.Count)
                {
                    counter = Sprites.Count;
                    Sleep = true;
                }
            }
        }

        private AnimationConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimatorControler(AnimationConfig config) //конструктор класса
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleep = false;

                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Trak == track).Sprites;
                    animation.counter = 0;

                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Trak == track).Sprites,
                    Loop = loop,
                    Speed = speed

                });
            }
        }

        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
                _activeAnimations.Remove(sprite);
        }


        public  void Update()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Update();
                if (animation.Value.counter < animation.Value.Sprites.Count)
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.counter];
            }
        }
        public void Dispose()
        {
            _activeAnimations.Clear();
        }

    }
}



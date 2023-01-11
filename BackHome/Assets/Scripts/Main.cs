using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BackHome
{
    public class Main : MonoBehaviour
    {
        
        [SerializeField] private InteractiveLevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;


       // private PlayerController _playerController;//- без физики контроллер
        private PlayerContrPhys _playerController;
        private CannonControlier _cannonControlier;
        private EmitterController _emitterController; 

        private void Awake()
        {
            // _playerController = new PlayerController(_playerView);
            _playerController = new PlayerContrPhys(_playerView);
            _cannonControlier = new CannonControlier(_cannonView._mazzlet, _playerView._transform);
            _emitterController = new EmitterController(_cannonView._bullets, _cannonView._emitter);
           
        }


        void Update()
        {

           _playerController.Update();
            _cannonControlier.Update();
            _emitterController.Update();
            


        }
    }
}


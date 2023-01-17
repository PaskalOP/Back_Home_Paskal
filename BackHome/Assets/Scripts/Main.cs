using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BackHome
{
    public class Main : MonoBehaviour
    {
        
        [SerializeField] private InteractiveLevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private GenegatorLevelView _generatorView;


       // private PlayerController _playerController;//- без физики контроллер
        private PlayerContrPhys _playerController;
        private CannonControlier _cannonControlier;
        private EmitterController _emitterController;
        private GeneratorController _generatorComtroller;
        private CameraController _cameraController;


        private void Awake()
        {
            // _playerController = new PlayerController(_playerView);
            _playerController = new PlayerContrPhys(_playerView);
            _cannonControlier = new CannonControlier(_cannonView._mazzlet, _playerView._transform);
            _emitterController = new EmitterController(_cannonView._bullets, _cannonView._emitter);
            _generatorComtroller = new GeneratorController(_generatorView);
            _cameraController = new CameraController(_playerView, Camera.main.transform);

            _generatorComtroller.Start();
        }


        void Update()
        {

           _playerController.Update();
            _cannonControlier.Update();
            _emitterController.Update();
            _cameraController.Update();


        }
    }
}


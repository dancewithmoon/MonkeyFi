using System;
using Base.AssetManagement;
using Base.Instantiating;
using UnityEngine;

namespace MonkeyBusiness.Services.InputService
{
    public class JoystickInputService : IInputService
    {
        private const string JoystickPath = "JoystickCanvas";
        
        private readonly IInstantiateService _instantiateService;
        private readonly IAssets _assets;
        private Joystick _joystick;
        
        public event Action OnInputStart;

        public bool IsInputting => _joystick && _joystick.IsInputting;
        public virtual Vector2 Axis => _joystick ? new Vector2(_joystick.Horizontal, _joystick.Vertical) : Vector2.zero;
        
        public JoystickInputService(IInstantiateService instantiateService, IAssets assets)
        {
            _instantiateService = instantiateService;
            _assets = assets;
            
            CreateJoystick();
        }

        private async void CreateJoystick()
        {
            var joystickPrefab = await _assets.Load<GameObject>(JoystickPath);
            GameObject joystick = _instantiateService.Instantiate(joystickPrefab);
            _joystick = joystick.GetComponentInChildren<Joystick>();
            _joystick.OnInputStart += HandleInputStart;
        }

        protected void HandleInputStart() => OnInputStart?.Invoke();
    }
}
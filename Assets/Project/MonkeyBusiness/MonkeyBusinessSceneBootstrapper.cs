using System;
using MonkeyBusiness.Services.InputService;
using UnityEngine;
using Zenject;

namespace MonkeyBusiness
{
    public class MonkeyBusinessSceneBootstrapper : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputService>().To(GetInputServiceType()).AsSingle();
        }
        
        private Type GetInputServiceType() =>
            Application.isEditor 
                ? typeof(StandaloneInputService) 
                : typeof(JoystickInputService);
    }
}
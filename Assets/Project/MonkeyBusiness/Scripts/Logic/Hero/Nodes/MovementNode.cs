using System;
using MonkeyBusiness.Services.InputService;
using UnityEngine;
using Zenject;

namespace MonkeyBusiness.Logic.Hero.Nodes
{
    [Serializable]
    public class MovementNode : HeroNode
    {
        private const float MinVelocityMagnitude = 0.01f;

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed = 4.0f;
        [SerializeField] private float _rotationSpeed = 20.0f;
        
        private IInputService _inputService;
        private Vector3 _movementVector;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Update()
        {
            TransformInputsToMovementVector();

            if (NeedToRotate())
                Rotate();
            
            Move();
            
            if (_inputService.IsInputting == false && _characterController.velocity.sqrMagnitude < MinVelocityMagnitude)
                stateMachine.Enter(HeroStateType.Idle);
        }
        
        private void TransformInputsToMovementVector() => 
            _movementVector = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y);

        private bool NeedToRotate() => 
            _movementVector.sqrMagnitude > MinVelocityMagnitude;
        
        private void Rotate() => 
            _characterController.transform.forward = 
                Vector3.MoveTowards(_characterController.transform.forward, _movementVector, _rotationSpeed * Time.deltaTime);

        private void Move() => 
            _characterController.Move(GetMovementVectorWithGravity() * Time.deltaTime);

        private Vector3 GetMovementVectorWithGravity() =>
            Physics.gravity + _movementSpeed * _movementVector;
    }
}
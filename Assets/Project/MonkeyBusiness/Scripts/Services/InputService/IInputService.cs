using System;
using UnityEngine;

namespace MonkeyBusiness.Services.InputService
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsInputting { get; }
        event Action OnInputStart;
    }
}
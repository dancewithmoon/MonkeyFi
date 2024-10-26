using Base.AssetManagement;
using Base.Instantiating;
using UnityEngine;

namespace MonkeyBusiness.Services.InputService
{
    public class StandaloneInputService : JoystickInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        public override Vector2 Axis
        {
            get
            {
                float horizontal = Input.GetAxis(HorizontalAxis);
                float vertical = Input.GetAxis(VerticalAxis);
                if (horizontal == 0 && vertical == 0)
                    return base.Axis;

                return new Vector2(horizontal, vertical);
            }
        }

        public StandaloneInputService(IInstantiateService instantiateService, IAssets assets) : base(instantiateService, assets)
        {
        }
    }
}
using UnityEngine;

namespace UI.Elements
{
    public abstract class BaseItem<TModel> : MonoBehaviour where TModel : class
    {
        protected TModel Model { get; private set; }
        
        public virtual void Initialize(TModel model)
        {
            Model = model;
        }

        public abstract void Draw();
    }
}
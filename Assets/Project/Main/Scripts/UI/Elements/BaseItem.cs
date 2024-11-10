using UnityEngine;

namespace UI.Elements
{
    public abstract class BaseItem<TModel> : MonoBehaviour where TModel : class
    {
        protected TModel Model { get; private set; }
        
        public virtual void Initialize(TModel model)
        {
            Model = model;
            OnItemShow();
        }

        public abstract void Draw();

        private void OnEnable()
        {
            if (Model != null)
                OnItemShow();
        }

        private void OnDisable() => OnItemHide();

        protected virtual void OnItemShow(){}
        
        protected virtual void OnItemHide(){}
    }
}
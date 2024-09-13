using UnityEngine;

namespace UI.Windows
{
    [RequireComponent(typeof(Canvas))]
    public class BaseWindow : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake() => 
            _canvas = GetComponent<Canvas>();

        public bool Visible
        {
            get => _canvas.enabled;
            set
            {
                _canvas.enabled = value;
                if(value)
                    OnWindowShow();
                else
                    OnWindowHide();
            }
        }

        private void Start() => OnWindowShow();

        protected virtual void OnWindowShow()
        {
        }

        protected virtual void OnWindowHide()
        {
        }

        public virtual void DrawWindow()
        {
            
        }
    }
}
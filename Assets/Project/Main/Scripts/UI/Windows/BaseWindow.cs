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
            set => _canvas.enabled = value;
        }

        public virtual void DrawWindow()
        {
            
        }
    }
}
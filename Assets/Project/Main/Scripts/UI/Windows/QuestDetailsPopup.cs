using Services.Quests;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class QuestDetailsPopup : PayloadedWindow<QuestModel>
    {
        [SerializeField] private Button _closeButton;
        private QuestModel _questModel;
        
        public override void SetPayload(QuestModel questModel) => _questModel = questModel;

        protected override void OnWindowShow() => 
            _closeButton.onClick.AddListener(Close);

        protected override void OnWindowHide() => 
            _closeButton.onClick.RemoveListener(Close);
    }
}
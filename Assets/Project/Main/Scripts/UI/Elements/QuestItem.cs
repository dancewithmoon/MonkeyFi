using Services;
using Services.Quests;
using TMPro;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Elements
{
    public class QuestItem : BaseItem<QuestModel>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _reward;
        [SerializeField] private Button _showQuestDetailsButton;
        [SerializeField] private Image _checkmark;
        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }
        
        public override void Draw()
        {
            _name.text = Model.Data.Name;
            _reward.text = Model.Data.Reward.ToString();
            _reward.gameObject.SetActive(!Model.Completed);
            _checkmark.gameObject.SetActive(Model.Completed);
        }

        protected override void OnItemShow()
        {
            _showQuestDetailsButton.onClick.AddListener(ShowQuestDetails);
            Model.OnQuestCompletedEvent += OnQuestCompleted;
        }
        
        protected override void OnItemHide()
        {
            _showQuestDetailsButton.onClick.RemoveListener(ShowQuestDetails);
            Model.OnQuestCompletedEvent -= OnQuestCompleted;
        }
        
        private void ShowQuestDetails() => 
            _windowService.ShowModalWindow(WindowType.QuestDetails, Model);
        
        private void OnQuestCompleted(QuestModel questModel) => Draw();
    }
}
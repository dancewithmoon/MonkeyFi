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
        }

        private void OnEnable() => 
            _showQuestDetailsButton.onClick.AddListener(ShowQuestDetails);

        private void OnDisable() => 
            _showQuestDetailsButton.onClick.RemoveListener(ShowQuestDetails);

        private void ShowQuestDetails() => 
            _windowService.ShowModalWindow(WindowType.QuestDetails, Model);
    }
}
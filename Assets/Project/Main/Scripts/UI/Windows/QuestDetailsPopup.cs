using System.Collections.Generic;
using Infrastructure.Factory;
using Services.Quests;
using Services.Quests.Conditions;
using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class QuestDetailsPopup : PayloadedWindow<QuestModel>
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _reward;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Transform _conditionsContainer;

        private IGameFactory _gameFactory;
        private QuestModel _questModel;
        private readonly Dictionary<ConditionModel, ConditionItem> _conditions = new();

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public override void SetPayload(QuestModel questModel) => 
            _questModel = questModel;

        public override void DrawWindow()
        {
            DrawName();
            DrawReward();
            DrawDescription();
            DrawConditions();
        }

        protected override void OnWindowShow()
        {
            _closeButton.onClick.AddListener(Close);
            _questModel.OnQuestCompletedEvent += OnQuestCompleted;
        }

        protected override void OnWindowHide()
        {
            _closeButton.onClick.RemoveListener(Close);
            _questModel.OnQuestCompletedEvent -= OnQuestCompleted;
        }

        private void DrawName() => 
            _name.text = _questModel.Data.Name;

        private void DrawReward() => 
            _reward.text = "Reward: " + _questModel.Data.Reward;

        private void DrawDescription() => 
            _description.text = _questModel.Data.Description;

        private void DrawConditions()
        {
            foreach ((ConditionModel model, ConditionItem item) in _conditions)
                item.gameObject.SetActive(false);

            _questModel.Conditions.ForEach(DrawConditionItem);
        }

        private async void DrawConditionItem(ConditionModel conditionModel)
        {
            if (_conditions.TryGetValue(conditionModel, out ConditionItem item) == false)
            {
                item = await _gameFactory.CreateConditionItem(conditionModel, _conditionsContainer);
                _conditions[conditionModel] = item;
            }

            item.gameObject.SetActive(true);
            item.Draw();
        }

        private void OnQuestCompleted(QuestModel questModel) => Close();
    }
}
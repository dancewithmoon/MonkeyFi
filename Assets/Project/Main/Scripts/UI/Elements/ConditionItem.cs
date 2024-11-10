using Services.Quests.Conditions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class ConditionItem : BaseItem<ConditionModel>
    {
        [SerializeField] private TMP_Text _conditionText;
        [SerializeField] private Button _completeButton;
        [SerializeField] private Image _checkmark;

        public override void Draw()
        {
            _conditionText.text = Model.Data.Name;
            _completeButton.gameObject.SetActive(!Model.Completed);
            _checkmark.gameObject.SetActive(Model.Completed);
        }

        protected override void OnItemShow()
        {
            _completeButton.onClick.AddListener(StartCompletion);
            Model.OnCompletedEvent += OnConditionCompleted;
        }

        protected override void OnItemHide()
        {
            _completeButton.onClick.RemoveListener(StartCompletion);
            Model.OnCompletedEvent -= OnConditionCompleted;
        }

        private void StartCompletion() => Model.StartCompletion();
        
        private void OnConditionCompleted(ConditionModel conditionModel) => Draw();
    }
}
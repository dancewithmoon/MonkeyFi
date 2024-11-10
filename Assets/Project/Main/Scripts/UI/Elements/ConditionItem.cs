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

        public override void Draw()
        {
            _conditionText.text = Model.Data.Name;
        }

        private void OnEnable() => 
            _completeButton.onClick.AddListener(Complete);

        private void OnDisable() => 
            _completeButton.onClick.RemoveListener(Complete);

        private void Complete() => Model.StartCompletion();
    }
}
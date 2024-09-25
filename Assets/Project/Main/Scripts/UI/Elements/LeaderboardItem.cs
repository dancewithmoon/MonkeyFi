using Services.Leaderboard;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class LeaderboardItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        
        private LeaderboardEntryModel _model;

        public void Initialize(LeaderboardEntryModel model)
        {
            _model = model;
        }

        public void Draw()
        {
            _name.text = _model.Name;
            _score.text = _model.Score.ToString();
        }
    }
}
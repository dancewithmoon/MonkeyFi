using Services.Leaderboard;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class LeaderboardItem : BaseItem<LeaderboardEntryModel>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public override void Draw()
        {
            _name.text = Model.Name;
            _score.text = Model.Score.ToString();
        }
    }
}
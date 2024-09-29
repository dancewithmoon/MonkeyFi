using System.Collections.Generic;
using MonkeyBusiness.Logic.Hero.Nodes;
using UnityEngine;

namespace MonkeyBusiness.Logic.Hero
{
    public class HeroState : MonoBehaviour
    {
        [SerializeField] private List<HeroNode> _nodes;
        
        public void Enter()
        {
            gameObject.SetActive(true);
            _nodes.ForEach(node => node.Enter());
        }

        public void Exit()
        {
            gameObject.SetActive(false);
            _nodes.ForEach(node => node.Exit());
        }
    }
}
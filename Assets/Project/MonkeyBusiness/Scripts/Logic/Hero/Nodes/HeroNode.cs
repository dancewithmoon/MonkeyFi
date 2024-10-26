using UnityEngine;

namespace MonkeyBusiness.Logic.Hero.Nodes
{
    public abstract class HeroNode : MonoBehaviour
    {
        [SerializeField] protected HeroStateMachine stateMachine;

        public virtual void Enter() => gameObject.SetActive(true);
        public virtual void Exit() => gameObject.SetActive(false);
    }
}
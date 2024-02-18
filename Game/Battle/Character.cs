using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Battle
{
    public interface ICharacter
    {
        CharacterInformation info { get; }
        CharacterEvents events { get; }
        CharacterControl ctr { get; }
        void UpdateBySec(double deltaTime);
        void SetEnemies(ICharacter[] enemies);
        void ChangeBehaviorState(CharacterBehaviorState state);
    }

    public class Character : ICharacter
    {
        public CharacterInformation info { get; private set; }
        public CharacterEvents events { get; private set; }
        public CharacterControl ctr { get; private set; }

        public Character(CharacterInformation information,
                         IAttackBehavior attackBehavior,
                         IAttackedDamage attackedDamage,
                         IRegenerateBehavior regenerateBehavior,
                         CharacterBehaviorState behaviorStateAfterDead)
        {
            info = information;
            events = new CharacterEvents();
            ctr = new CharacterControl(
                info, info.state, events, 
                attackBehavior, attackedDamage,
                regenerateBehavior,
                behaviorStateAfterDead);
        }

        public void UpdateBySec(double deltaTime)
        {
            ctr.UpdateBySec(deltaTime);
        }

        public void SetEnemies(ICharacter[] enemies)
        {
            ctr.SetEnemies(enemies);
        }

        public void ChangeBehaviorState(CharacterBehaviorState state)
        {
            info.state.BehaviorState = state;
        }
    }
}
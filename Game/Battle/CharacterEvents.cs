using yayu.Event;

namespace yayu.Battle
{
    public class CharacterEvents
    {
        private CustomEvent<double> onAttack = new CustomEvent<double>();
        internal IEventTrigger<double> OnAttackTrigger => onAttack;
        /// <summary>
        /// Event triggered when the character performs an attack.
        /// <para>Argument: double - Represents the strength or effect of the attack.</para>
        /// </summary> 
        public IEventSubscription<double> OnAttack => onAttack;



        private CustomEvent<double, object> onAttacked = new CustomEvent<double, object>();
        internal IEventTrigger<double, object> OnAttackedTrigger => onAttacked;
        /// <summary>
        /// Event triggered when the character is attacked.
        /// <para>Argument: double - Represents the strength or impact of the received attack.</para>
        /// </summary>
        public IEventSubscription<double, object> OnAttacked => onAttacked;



        private CustomEvent<double> onDamaged = new CustomEvent<double>();
        internal IEventTrigger<double> OnDamagedTrigger => onDamaged;
        /// <summary>
        /// Event triggered when the character takes damage.
        /// <para>Argument: double - Represents the amount of damage taken.</para>
        /// </summary>
        public IEventSubscription<double> OnDamaged => onDamaged;



        private CustomEvent<double> onRegenerate = new CustomEvent<double>();
        internal IEventTrigger<double> OnRegenerateTrigger => onRegenerate;
        /// <summary>
        /// Event triggered when the character regenerates health or mana.
        /// <para>Argument: double - Represents the amount regenerated.</para>
        /// </summary>
        public IEventSubscription<double> OnRegenerate => onRegenerate;




        private CustomEvent onDead = new CustomEvent();
        internal IEventTrigger OnDeadTrigger => onDead;
        /// <summary>
        /// Event triggered when the character dies.
        /// <para>Argument: CharacterInformation - Contains information about the deceased character.</para>
        /// </summary>
        public IEventSubscription OnDead => onDead;
    }
}
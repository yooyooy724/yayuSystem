using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Battle
{
    public class CharacterInformation
    {
        // Unique identifier for the character (like name, ID, etc.)
        public CharacterIdentifier id { get; }

        // Current state of the character (like behavior states: battling, resting, etc.)
        public CharacterState state { get; }

        // parameters
        public Func<double> MaxHp { get; }

        public CharacterInformation(CharacterIdentifier id, CharacterState state, Func<double> MaxHp)
        {
            this.id = id;
            this.state = state;
            this.MaxHp = MaxHp;
        }

        public static CharacterInformation InstantCreate(double maxHp, string name)
        {
            CharacterIdentifier id = CharacterIdentifier.CreateWithGuid(name);
            CharacterState state = new CharacterState(maxHp, CharacterBehaviorState.Dying);
            return new CharacterInformation(id, state, () => maxHp);
        }
    }

    public class CharacterIdentifier
    {
        public CharacterIdentifier(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public string name { get; }
        public int id { get; }

        public static CharacterIdentifier CreateWithGuid(string name = "character")
        {
            return new CharacterIdentifier(name, new Guid().GetHashCode());
        }
    }
}
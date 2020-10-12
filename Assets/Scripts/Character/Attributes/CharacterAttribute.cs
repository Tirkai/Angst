using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Attributes
{
    public class CharacterAttribute
    {

        public CharacterAttributeType Type { get; }

        float baseAmount;

        public float BaseAmount { get => baseAmount; set => baseAmount = value; }

        float minAmount = 0;
        public float MinAmount { get => minAmount; set => minAmount = value; }

        float maxAmount = float.PositiveInfinity;
        public float MaxAmount { get => maxAmount; set => maxAmount = value; }

        float amount = 0;
        public float Amount {
            get => amount;
            set {
                if (value > MinAmount)
                {
                    if (value < MaxAmount) amount = value;
                    else amount = MaxAmount;
                }
                else amount = MinAmount;
            }
        }

        public CharacterAttribute(CharacterAttributeType type, float baseAmount)
        {
            Type = type;
            BaseAmount = baseAmount;
            Amount = baseAmount;
        }
    }
}


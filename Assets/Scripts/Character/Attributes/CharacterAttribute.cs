using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Attributes
{
    public class CharacterAttribute
    {
        public float BaseAmount { get; set; }
        public float MinAmount { get; set; }
        public float MaxAmount { get; set; }

        float amount = 0;
        public float Amount {
            get => amount;
            set {
                if (value >= MinAmount)
                {
                    amount = value;

                }
                else amount = MinAmount;
            }
        }

        public CharacterAttribute(float baseAmount)
        {
            MinAmount = 0;
            BaseAmount = baseAmount;
            Amount = baseAmount;
            MaxAmount = float.PositiveInfinity;
        }
    }
}


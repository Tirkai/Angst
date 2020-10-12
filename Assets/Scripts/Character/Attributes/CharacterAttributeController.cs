using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Character.Attributes;
using System.Collections;

namespace Character.Attributes
{
    class CharacterAttributeController : MonoBehaviour
    {

        CharacterDamageController characterDamage;

        public CharacterAttribute healthAmount = new CharacterAttribute(
           type: CharacterAttributeType.HealthAmount,
           baseAmount: 100
        );
        public CharacterAttribute healthMaximumAmount = new CharacterAttribute(
            type: CharacterAttributeType.HealthMaximumAmount,
            baseAmount: 100
        );

        public CharacterAttribute healthRegenerationAmount = new CharacterAttribute(
            type: CharacterAttributeType.HealthRegeneratonAmount,
            baseAmount: 0
        );

        public CharacterAttribute healthRegenerationRate = new CharacterAttribute(
            type: CharacterAttributeType.HealthRegenerationRate,
            baseAmount: 1
        );

        public CharacterAttribute energyShieldAmount = new CharacterAttribute(
            type: CharacterAttributeType.EnergyShieldAmount,
            baseAmount: 75
        );

        public CharacterAttribute energyShieldMaximumAmount = new CharacterAttribute(
            type: CharacterAttributeType.EnergyShieldMaximumAmount,
            baseAmount: 75
        );

        public CharacterAttribute energyShieldRechargeAmount = new CharacterAttribute(
            type: CharacterAttributeType.EnergyShieldRechargeAmount,
            baseAmount: 1
        );

        public CharacterAttribute energyShieldRechargeRate = new CharacterAttribute(
            type: CharacterAttributeType.EnergyShieldRechargeRate,
            baseAmount: 1
        );

        public CharacterAttribute energyShieldDelay = new CharacterAttribute(
            type: CharacterAttributeType.EnergyShieldDelay,
            baseAmount: 0
        );

        public CharacterAttribute energyShieldStartFasterRate = new CharacterAttribute(
            type: CharacterAttributeType.EnergyShieldStartFasterRate,
            baseAmount: 1
        );

        public CharacterAttribute staminaAmount = new CharacterAttribute(
            type: CharacterAttributeType.StaminaAmount,
            baseAmount: 50
        );

        public CharacterAttribute staminaMaximumAmount = new CharacterAttribute(
            type: CharacterAttributeType.StaminaMaximumAmount,
            baseAmount: 50
        );

        public CharacterAttribute staminaRegenerationAmount = new CharacterAttribute(
            type: CharacterAttributeType.StaminaRegenerationAmount,
            baseAmount: 5
        );

        public CharacterAttribute staminaRegenerationRate = new CharacterAttribute(
            type: CharacterAttributeType.StaminaRegenerationRate,
            baseAmount: 1
        );

        public CharacterAttribute armourAmount = new CharacterAttribute(
            type: CharacterAttributeType.ArmourAmount,
            baseAmount: 10
        );

        public CharacterAttribute armourQuality = new CharacterAttribute(
            type: CharacterAttributeType.ArmourQuality,
            baseAmount: 1
        );

        public CharacterAttribute moveSpeed = new CharacterAttribute(
            type: CharacterAttributeType.MoveSpeed,
            baseAmount: 3
        );

        void Start()
        {
            characterDamage = GetComponent<CharacterDamageController>();
            characterDamage.OnTakeDamage += (Damage damage) => { StopEnergyShieldRecharge(); };


            staminaAmount.MinAmount = -100;

            StartCoroutine(HealthCoroutine());
            StartCoroutine(EnergyShieldCoroutine());
            StartCoroutine(StaminaCoroutine());
        }

        void StopEnergyShieldRecharge()
        {
            energyShieldDelay.Amount = 4 * (1 / energyShieldStartFasterRate.Amount);
        }

        IEnumerator HealthCoroutine()
        {
            while (true)
            {

                if (healthAmount.Amount < healthMaximumAmount.Amount)
                {
                    healthAmount.Amount += (healthRegenerationAmount.Amount * healthRegenerationRate.Amount) * 0.05f;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator EnergyShieldCoroutine()
        {
            while (true)
            {

                if(energyShieldDelay.Amount <= 0)
                {
                    if (energyShieldAmount.Amount < energyShieldMaximumAmount.Amount)
                    {
                        energyShieldAmount.Amount += (energyShieldRechargeAmount.Amount * energyShieldRechargeRate.Amount) * 0.05f;
                    }

                } else
                {
                    energyShieldDelay.Amount -= 0.05f * energyShieldStartFasterRate.Amount;
                }

                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator StaminaCoroutine()
        {
            while (true)
            {

                if (staminaAmount.Amount < staminaMaximumAmount.Amount)
                {
                    staminaAmount.Amount += (staminaRegenerationAmount.Amount * staminaRegenerationRate.Amount) * 0.05f;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}

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
        public Dictionary<DynamicAttributeType, Attribute> dynamicAttributes = new Dictionary<DynamicAttributeType, Attribute>();
        public Dictionary<ScalableAttributeType, Attribute> scalableAttributes = new Dictionary<ScalableAttributeType, Attribute>();



        void Start()
        {


            characterDamage = GetComponent<CharacterDamageController>();

           

            scalableAttributes.Add(ScalableAttributeType.HealthMaximumAmount, new Attribute(baseAmount: 100));
            scalableAttributes.Add(ScalableAttributeType.HealthRegeneratonAmount, new Attribute(baseAmount: 0));
            scalableAttributes.Add(ScalableAttributeType.HealthRegenerationRate, new Attribute(baseAmount: 1));
            scalableAttributes.Add(ScalableAttributeType.EnergyShieldMaximumAmount, new Attribute(baseAmount: 75));
            scalableAttributes.Add(ScalableAttributeType.EnergyShieldRechargeAmount, new Attribute(baseAmount: 1));
            scalableAttributes.Add(ScalableAttributeType.EnergyShieldRechargeRate, new Attribute(baseAmount: 1));
            scalableAttributes.Add(ScalableAttributeType.EnergyShieldStartFasterRate, new Attribute(baseAmount: 1));
            scalableAttributes.Add(ScalableAttributeType.StaminaMaximumAmount, new Attribute(baseAmount: 100));
            scalableAttributes.Add(ScalableAttributeType.StaminaRegenerationAmount, new Attribute(baseAmount: 20));
            scalableAttributes.Add(ScalableAttributeType.StaminaRegenerationRate, new Attribute(baseAmount: 1));
            scalableAttributes.Add(ScalableAttributeType.ArmourAmount, new Attribute(baseAmount: 0));
            scalableAttributes.Add(ScalableAttributeType.ArmourQuality, new Attribute(baseAmount: 1));
            scalableAttributes.Add(ScalableAttributeType.MovementSpeed, new Attribute(baseAmount: 10));


            dynamicAttributes.Add(DynamicAttributeType.HealthAmount, new Attribute(
                baseAmount: scalableAttributes[ScalableAttributeType.HealthMaximumAmount].Amount
            ));
            dynamicAttributes.Add(DynamicAttributeType.EnergyShieldAmount, new Attribute(
                baseAmount: scalableAttributes[ScalableAttributeType.EnergyShieldMaximumAmount].Amount
            ));
            dynamicAttributes.Add(DynamicAttributeType.StaminaAmount, new Attribute(
                baseAmount: scalableAttributes[ScalableAttributeType.StaminaMaximumAmount].Amount
            ));
            dynamicAttributes.Add(DynamicAttributeType.EnergyShieldDelay, new Attribute(baseAmount: 4));

            dynamicAttributes[DynamicAttributeType.StaminaAmount].MinAmount = -100;

            characterDamage.OnTakeDamage += StopEnergyShieldRecharge;

            StartCoroutine(HealthCoroutine());
            StartCoroutine(EnergyShieldCoroutine());
            StartCoroutine(StaminaCoroutine());
        }

        void StopEnergyShieldRecharge(Damage damage)
        {
            var energyShieldDelay = dynamicAttributes[DynamicAttributeType.EnergyShieldDelay];
            energyShieldDelay.Amount = energyShieldDelay.BaseAmount * (1 / scalableAttributes[ScalableAttributeType.EnergyShieldStartFasterRate].Amount);
        }

        IEnumerator HealthCoroutine()
        {
            while (true)
            {
                var healthAmount = dynamicAttributes[DynamicAttributeType.HealthAmount];
                var healthMaximumAmount = scalableAttributes[ScalableAttributeType.HealthMaximumAmount];
                var healthRegenerationAmount = scalableAttributes[ScalableAttributeType.HealthRegeneratonAmount];
                var healthRegenerationRate = scalableAttributes[ScalableAttributeType.HealthRegenerationRate];

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
                var energyShieldAmount = dynamicAttributes[DynamicAttributeType.EnergyShieldAmount];
                var energyShieldDelay = dynamicAttributes[DynamicAttributeType.EnergyShieldDelay];
                var energyShieldMaximumAmount = scalableAttributes[ScalableAttributeType.EnergyShieldMaximumAmount];
                var energyShieldRechargeAmount = scalableAttributes[ScalableAttributeType.EnergyShieldRechargeAmount];
                var energyShieldRechargeRate = scalableAttributes[ScalableAttributeType.EnergyShieldRechargeRate];
                var energyShieldStartFasterRate = scalableAttributes[ScalableAttributeType.EnergyShieldStartFasterRate];
                if (energyShieldDelay.Amount <= 0)
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
                var staminaAmount = dynamicAttributes[DynamicAttributeType.StaminaAmount];
                var staminaMaximumAmount = scalableAttributes[ScalableAttributeType.StaminaMaximumAmount];
                var staminaRegenerationAmount = scalableAttributes[ScalableAttributeType.StaminaRegenerationAmount];
                var staminaRegenerationRate = scalableAttributes[ScalableAttributeType.StaminaRegenerationRate];
                if (staminaAmount.Amount < staminaMaximumAmount.Amount)
                {
                    staminaAmount.Amount += (staminaRegenerationAmount.Amount * staminaRegenerationRate.Amount) * 0.05f;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}

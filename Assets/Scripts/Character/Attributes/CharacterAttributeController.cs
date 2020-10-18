using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Character.Attributes;
using System.Collections;

class CharacterAttributeController : MonoBehaviour
{
    CharacterDamageController characterDamage;
    CharacterStatusEffectController characterStatusEffect;
    public Dictionary<DynamicAttributeType, CharacterAttribute> dynamicAttributes = new Dictionary<DynamicAttributeType, CharacterAttribute>();
    public Dictionary<ScalableAttributeType, CharacterAttribute> scalableAttributes = new Dictionary<ScalableAttributeType, CharacterAttribute>();
    public List<IAttributeModifier> statusEffectModifiers = new List<IAttributeModifier>();
    void Start()
    {
        characterDamage = GetComponent<CharacterDamageController>();
        characterStatusEffect = GetComponent<CharacterStatusEffectController>();

        scalableAttributes.Add(ScalableAttributeType.HealthMaximumAmount, new CharacterAttribute(
            baseAmount: 100
        ));
        scalableAttributes.Add(ScalableAttributeType.HealthRegeneratonAmount, new CharacterAttribute(
            baseAmount: 0
        ));
        scalableAttributes.Add(ScalableAttributeType.HealthRegenerationRate, new CharacterAttribute(
            baseAmount: 1
        ));
        scalableAttributes.Add(ScalableAttributeType.EnergyShieldMaximumAmount, new CharacterAttribute(
            baseAmount: 175
        ));
        scalableAttributes.Add(ScalableAttributeType.EnergyShieldRechargeAmount, new CharacterAttribute(
            baseAmount: 1
        ));
        scalableAttributes.Add(ScalableAttributeType.EnergyShieldRechargeRate, new CharacterAttribute(
            baseAmount: 1
        ));
        scalableAttributes.Add(ScalableAttributeType.EnergyShieldStartFasterRate, new CharacterAttribute(
            baseAmount: 1
        ));
        scalableAttributes.Add(ScalableAttributeType.StaminaMaximumAmount, new CharacterAttribute(
            baseAmount: 100
        ));
        scalableAttributes.Add(ScalableAttributeType.StaminaRegenerationAmount, new CharacterAttribute(
            baseAmount: 20
        ));
        scalableAttributes.Add(ScalableAttributeType.StaminaRegenerationRate, new CharacterAttribute(
            baseAmount: 1
        ));
        scalableAttributes.Add(ScalableAttributeType.ArmourAmount, new CharacterAttribute(
            baseAmount: 0
        ));
        scalableAttributes.Add(ScalableAttributeType.ArmourQuality, new CharacterAttribute(
            baseAmount: 1
        ));
        scalableAttributes.Add(ScalableAttributeType.MovementSpeed, new CharacterAttribute(
            baseAmount: 5
        ));

        dynamicAttributes.Add(DynamicAttributeType.HealthAmount, new CharacterAttribute(
            baseAmount: scalableAttributes[ScalableAttributeType.HealthMaximumAmount].Amount
        ));
        dynamicAttributes.Add(DynamicAttributeType.EnergyShieldAmount, new CharacterAttribute(
            baseAmount: scalableAttributes[ScalableAttributeType.EnergyShieldMaximumAmount].Amount
        ));
        dynamicAttributes.Add(DynamicAttributeType.StaminaAmount, new CharacterAttribute(
            baseAmount: scalableAttributes[ScalableAttributeType.StaminaMaximumAmount].Amount
        ));
        dynamicAttributes.Add(DynamicAttributeType.EnergyShieldDelay, new CharacterAttribute(
            baseAmount: 4
        ));

        dynamicAttributes[DynamicAttributeType.StaminaAmount].MinAmount = -100;

        characterDamage.TakedDamage += OnTakedDamage;
        characterStatusEffect.ChangedStatusEffect += OnChangeStatusEffect;

        StartCoroutine(HealthCoroutine());
        StartCoroutine(EnergyShieldCoroutine());
        StartCoroutine(StaminaCoroutine());

        dynamicAttributes[DynamicAttributeType.EnergyShieldAmount].Amount = 0;
    }

    void OnTakedDamage(Damage damage)
    {
        StopEnergyShieldRecharge();
        
        var modifiers1 = new List<IAttributeModifier>();
        modifiers1.Add(new SimpleAttributeModifier(ScalableAttributeType.HealthRegeneratonAmount, SimpleModifierType.Add, 1f));
        //modifiers1.Add(new SimpleAttributeModifier(ScalableAttributeType.HealthRegeneratonAmount, SimpleModifierType.Add, 1f));

        //modifiers1.Add(new SimpleAttributeModifier(ScalableAttributeType.HealthRegeneratonAmount, SimpleModifierType.Add, 10));
        //modifiers1.Add(new SimpleAttributeModifier(ScalableAttributeType.HealthMaximumAmount, SimpleModifierType.Increase, 1f));



        var modifiers2 = new List<IAttributeModifier>();
         // modifiers2.Add(new SimpleAttributeModifier(ScalableAttributeType.HealthRegeneratonAmount, SimpleModifierType.Add, 1f));
       // modifiers2.Add(new SimpleAttributeModifier(ScalableAttributeType.EnergyShieldStartFasterRate, SimpleModifierType.Less, 0.1f));

        //modifiers2.Add(new SimpleAttributeModifier(ScalableAttributeType.MovementSpeed, SimpleModifierType.Less, 0.1f));


        // TEST
        characterStatusEffect.AddStatusEffect(new StatusEffect(modifiers: modifiers1, isDurationable: true, duration: 10, maxStackSize: 1, key: "Test"));
        characterStatusEffect.AddStatusEffect(new StatusEffect(modifiers: modifiers2, isDurationable: true, duration: 4, maxStackSize: 1, key: "Kek"));


        Debug.Log("HP: " + scalableAttributes[ScalableAttributeType.HealthMaximumAmount].Amount.ToString());
    }

    void StopEnergyShieldRecharge()
    {
        if(dynamicAttributes[DynamicAttributeType.EnergyShieldDelay].Amount <= 0)
        {
            var energyShieldDelay = dynamicAttributes[DynamicAttributeType.EnergyShieldDelay];
            energyShieldDelay.Amount = energyShieldDelay.BaseAmount * (1 / scalableAttributes[ScalableAttributeType.EnergyShieldStartFasterRate].Amount);
        }
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

            }
            else
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

    void OnChangeStatusEffect(StatusEffect statusEffect, List<StatusEffect> allStatusEffects)
    {
        List<IAttributeModifier> modifiers = new List<IAttributeModifier>();
        foreach(var status in allStatusEffects)
        {
            if (!status.IsExpired)
            {
                foreach(var modifier in status.modifiers)
                {
                    modifiers.Add(modifier);
                }
            }
        }
        statusEffectModifiers = modifiers;
        Debug.Log("ChangeStatusEffect " + statusEffectModifiers.Count.ToString());
        RecalculateAttributeValuesWithModifiers();

    }


    void RecalculateAttributeValuesWithModifiers()
    {
        // MERGE
        List<IAttributeModifier> modifiers = statusEffectModifiers;

        foreach(var item in scalableAttributes)
        {
            CharacterAttribute attribute = item.Value;
            List<IAttributeModifier> mods = modifiers.FindAll(mod => mod.AttributeType == item.Key);

            Dictionary<SimpleModifierType, float> values = new Dictionary<SimpleModifierType, float>();

            List<SimpleAttributeModifier> simpleModifiers = mods
                .Where(x => x is SimpleAttributeModifier)
                .Select(x => (SimpleAttributeModifier)x)
                .ToList();

            float GetAccumulatedAttributeValue(SimpleModifierType type) => simpleModifiers
                .Where(x => x.Type == type)
                .Select(x => x.Amount)
                .Aggregate(0f, (acc, x) => (acc + x));

            float GetExactMinimalAttributeValue(SimpleModifierType type)
            {
                var exactModifiers = simpleModifiers.Where(x => x.Type == type).ToList();
                if (exactModifiers.Count > 0)
                {
                    return exactModifiers
                        .Select(x => x.Amount)
                        .Min();
                } else
                {
                    return -1;
                }
            }

            values.Add(SimpleModifierType.Add, GetAccumulatedAttributeValue(SimpleModifierType.Add));
            values.Add(SimpleModifierType.Increase, GetAccumulatedAttributeValue(SimpleModifierType.Increase));
            values.Add(SimpleModifierType.More, GetAccumulatedAttributeValue(SimpleModifierType.More));
            values.Add(SimpleModifierType.Less, GetAccumulatedAttributeValue(SimpleModifierType.Less));
            values.Add(SimpleModifierType.Remove, GetAccumulatedAttributeValue(SimpleModifierType.Remove));
            values.Add(SimpleModifierType.Set, GetExactMinimalAttributeValue(SimpleModifierType.Set));

            float totalValue;
            if(values[SimpleModifierType.Set] <= -1)
            {
                float increasedValue = (attribute.BaseAmount + values[SimpleModifierType.Add]) *
                    (1 + values[SimpleModifierType.Increase]) *
                    (1 + values[SimpleModifierType.More]);

                float decreasedValue = (increasedValue * values[SimpleModifierType.Less]) + values[SimpleModifierType.Remove];
                totalValue = increasedValue - decreasedValue;
            } else
            {
                totalValue = values[SimpleModifierType.Set];
            }

            attribute.Amount = totalValue;
        }
        
        if(dynamicAttributes[DynamicAttributeType.HealthAmount].Amount >= scalableAttributes[ScalableAttributeType.HealthMaximumAmount].Amount)
        {
            dynamicAttributes[DynamicAttributeType.HealthAmount].Amount = scalableAttributes[ScalableAttributeType.HealthMaximumAmount].Amount;
        }

        if (dynamicAttributes[DynamicAttributeType.EnergyShieldAmount].Amount >= scalableAttributes[ScalableAttributeType.EnergyShieldMaximumAmount].Amount)
        {
            dynamicAttributes[DynamicAttributeType.EnergyShieldAmount].Amount = scalableAttributes[ScalableAttributeType.EnergyShieldMaximumAmount].Amount;
        }

        if (dynamicAttributes[DynamicAttributeType.StaminaAmount].Amount >= scalableAttributes[ScalableAttributeType.StaminaMaximumAmount].Amount)
        {
            dynamicAttributes[DynamicAttributeType.StaminaAmount].Amount = scalableAttributes[ScalableAttributeType.StaminaMaximumAmount].Amount;
        }

    }
}
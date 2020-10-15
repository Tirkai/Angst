using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.Attributes;

public class CharacterDamageController : MonoBehaviour
{

    public delegate void DamageHandler(Damage damage);
    public event DamageHandler TakedDamage;

    CharacterAttributeController attributes;

    void Start()
    {
        attributes = GetComponent<CharacterAttributeController>();
    
    }

    public void TakeDamage(Damage damage)
    {
        TakedDamage?.Invoke(damage);

        float initialDamage = damage.Amount;
        CharacterAttribute energySheild = attributes.dynamicAttributes[DynamicAttributeType.EnergyShieldAmount];
        CharacterAttribute health = attributes.dynamicAttributes[DynamicAttributeType.HealthAmount];
        CharacterAttribute armour = attributes.scalableAttributes[ScalableAttributeType.ArmourAmount];
        CharacterAttribute armourQuality = attributes.scalableAttributes[ScalableAttributeType.ArmourQuality];

        float damageWithConsumedEnergyShield = initialDamage;

        if (damage.Amount <= energySheild.Amount) {
            energySheild.Amount -= initialDamage;
            damageWithConsumedEnergyShield = 0;
        }
        else
        {
            damageWithConsumedEnergyShield -= energySheild.Amount;
            energySheild.Amount = 0;
        }

        float damageWithArmourReduction = damageWithConsumedEnergyShield * 0.7f;
        float damageAvoidedArmour = damageWithConsumedEnergyShield * 0.3f;

        if (damageWithArmourReduction >= armour.Amount * armourQuality.Amount)
        {
            damageWithArmourReduction -= armour.Amount * armourQuality.Amount;
        } else
        {
            damageWithArmourReduction = 0;
        }

        float totalDamage = damageWithArmourReduction + damageAvoidedArmour;

        if(initialDamage > 0)
        {
            health.Amount -= totalDamage;
        }
        Debug.Log("Damage " + totalDamage.ToString());

    }

}

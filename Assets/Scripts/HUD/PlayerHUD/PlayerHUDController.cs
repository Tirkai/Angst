using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Character.Attributes;

public class PlayerHUDController : MonoBehaviour
{

    CharacterAttributeController attributes;

    public Slider healthSlider;
    public Slider energyShieldSlider;
    public Slider staminaSlider;
    public Text healthText;
    public Text energyShieldText;



    void Start()
    {
        attributes = GetComponent<CharacterAttributeController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attribute healthAmount = attributes.dynamicAttributes[DynamicAttributeType.HealthAmount];
        Attribute energyShieldAmount = attributes.dynamicAttributes[DynamicAttributeType.EnergyShieldAmount];
        Attribute staminaAmount = attributes.dynamicAttributes[DynamicAttributeType.StaminaAmount];
        Attribute healthMaximumAmount = attributes.scalableAttributes[ScalableAttributeType.HealthMaximumAmount];
        Attribute energyShieldMaximumAmount = attributes.scalableAttributes[ScalableAttributeType.EnergyShieldMaximumAmount];
        Attribute staminaMaximumAmount = attributes.scalableAttributes[ScalableAttributeType.StaminaMaximumAmount];

        Debug.Log(staminaMaximumAmount.BaseAmount);

        healthSlider.value = healthAmount.Amount;
        energyShieldSlider.value = energyShieldAmount.Amount;

        healthSlider.maxValue = healthMaximumAmount.Amount;


        if (healthMaximumAmount.Amount > energyShieldMaximumAmount.Amount)
        {
            energyShieldSlider.maxValue = healthMaximumAmount.Amount;
        } else
        {
            energyShieldSlider.maxValue = energyShieldMaximumAmount.Amount;
        }

        staminaSlider.maxValue = staminaMaximumAmount.Amount;
        staminaSlider.value = staminaAmount.Amount;

        healthText.text = Mathf.Floor(healthAmount.Amount).ToString();

        if(energyShieldAmount.Amount > energyShieldMaximumAmount.Amount * 0.15f)
        {
            energyShieldText.text = Mathf.Floor(energyShieldAmount.Amount).ToString();
        } else
        {
            energyShieldText.text = "";
        }



    }
}

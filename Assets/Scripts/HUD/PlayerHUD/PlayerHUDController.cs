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
        healthSlider.value = attributes.healthAmount.Amount;
        energyShieldSlider.value = attributes.energyShieldAmount.Amount;

        healthSlider.maxValue = attributes.healthMaximumAmount.Amount;


        if (attributes.healthMaximumAmount.Amount > attributes.energyShieldMaximumAmount.Amount)
        {
            energyShieldSlider.maxValue = attributes.healthMaximumAmount.Amount;
        } else
        {
            energyShieldSlider.maxValue = attributes.energyShieldMaximumAmount.Amount;
        }

        staminaSlider.maxValue = attributes.staminaMaximumAmount.Amount;
        staminaSlider.value = attributes.staminaAmount.Amount;

        healthText.text = Mathf.Floor(attributes.healthAmount.Amount).ToString();

        if(attributes.energyShieldAmount.Amount > attributes.energyShieldMaximumAmount.Amount * 0.15f)
        {
            energyShieldText.text = Mathf.Floor(attributes.energyShieldAmount.Amount).ToString();
        } else
        {
            energyShieldText.text = "";
        }



    }
}

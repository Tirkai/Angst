using Character.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    CharacterAttributeController attributes;
    float moveHorizontal;
    float moveVertical;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        attributes = GetComponent<CharacterAttributeController>();
    }

    void Update()
    {
        CharacterAttribute movementSpeed = attributes.scalableAttributes[ScalableAttributeType.MovementSpeed];

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        playerRigidbody.velocity = new Vector2(moveHorizontal * movementSpeed.Amount, moveVertical * movementSpeed.Amount);

        if (Input.GetKeyDown(KeyCode.Space)) Dash();
    }

    void Dash()
    {
        CharacterAttribute staminaAmount = attributes.dynamicAttributes[DynamicAttributeType.StaminaAmount];

        if (staminaAmount.Amount > 0)
        {
            staminaAmount.Amount -= 25;
            playerRigidbody.AddRelativeForce(new Vector2(moveHorizontal, moveVertical) * 5000);
        }
    }
}

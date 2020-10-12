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
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        playerRigidbody.velocity = new Vector2(moveHorizontal * attributes.moveSpeed.Amount, moveVertical * attributes.moveSpeed.Amount);

        if (Input.GetKeyDown(KeyCode.Space)) Dash();
    }

    void Dash()
    {
        if(attributes.staminaAmount.Amount > 0)
        {
            attributes.staminaAmount.Amount -= 25;
            playerRigidbody.AddRelativeForce(new Vector2(moveHorizontal, moveVertical) * 5000);
        }
    }
}

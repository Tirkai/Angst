using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedArea : MonoBehaviour
{
    public DamageType damageType;
    public float damage;

    List<CharacterDamageController> characterDamageControllers = new List<CharacterDamageController>();

    void Awake()
    {
        StartCoroutine(DamageCoroutine());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger");
        CharacterDamageController characterDamage = collider.GetComponent<CharacterDamageController>();
        if (characterDamage)
        {
            characterDamageControllers.Add(characterDamage);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Trigger");
        CharacterDamageController characterDamage = collider.GetComponent<CharacterDamageController>();
        if (characterDamage)
        {
            characterDamageControllers.Remove(characterDamage);
        }
    }

    IEnumerator DamageCoroutine()
    {
        while (true)
        {
            Debug.Log(characterDamageControllers.Count);
            foreach(var item in characterDamageControllers)
            {
                item.TakeDamage(new Damage(damageType, damage * 0.1f));
            }
            yield return new WaitForSeconds(0.1f);

        }
    }
}

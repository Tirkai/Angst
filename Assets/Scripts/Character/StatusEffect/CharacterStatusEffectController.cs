using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatusEffectController : MonoBehaviour
{
    public delegate void ChangeStatusEffectHandler(StatusEffect statusEffect, List<StatusEffect> allStatusEffects);
    public event ChangeStatusEffectHandler ChangedStatusEffect;

    List<StatusEffect> allStatusEffects = new List<StatusEffect>();
   
    public void AddStatusEffect(StatusEffect statusEffect)
    {
        var findedItems = allStatusEffects.FindAll(item => item.Key == statusEffect.Key);
        if (findedItems.Count >= statusEffect.MaxStackSize)
        {
            allStatusEffects.Remove(findedItems[0]);
        }
        allStatusEffects.Add(statusEffect);

        ChangedStatusEffect.Invoke(statusEffect, allStatusEffects);
    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {  
        allStatusEffects.Remove(statusEffect);
        ChangedStatusEffect.Invoke(statusEffect, allStatusEffects);
    }

    void Start()
    {
        StartCoroutine(StatusEffectsTick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StatusEffectsTick()
    {
        while (true)
        {
            Debug.Log("Count " + allStatusEffects.Count.ToString());

            var iteratedList = allStatusEffects.ToList();
            foreach(var item in iteratedList)
            {
                if (!item.IsExpired)
                {
                   
                    if (item.IsDurationable)
                    {
                        if (item.Duration > 0)
                        {
                            item.Duration -= 1;
                            Debug.Log(item.Duration);
                        }
                        else
                        {
                            item.IsExpired = true;
                            RemoveStatusEffect(item);
                        }
                    
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
   
}

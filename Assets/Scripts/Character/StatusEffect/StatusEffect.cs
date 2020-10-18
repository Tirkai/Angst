using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StatusEffect
{
    public float Duration { get; set; }
    public bool IsDurationable { get; set; }
    public List<IAttributeModifier> modifiers = new List<IAttributeModifier>();
    public bool IsExpired {get; set; }

    public int MaxStackSize { get; set; }

    public string Key { get; set; }
    public StatusEffect(List<IAttributeModifier> modifiers, bool isDurationable, float duration = 0, int maxStackSize = 1, string key = "")
    {
        this.modifiers = modifiers;
        Duration = duration;
        IsDurationable = isDurationable;
        MaxStackSize = maxStackSize;
        Key = key;
    }
}
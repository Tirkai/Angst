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

    public bool IsStackable { get; set; }

    public string Key { get; set; }
    public StatusEffect(List<IAttributeModifier> modifiers, bool isDurationable, float duration = 0, bool isStackable = false, string key = "")
    {
        this.modifiers = modifiers;
        Duration = duration;
        IsDurationable = isDurationable;
        IsStackable = isStackable;
        Key = key;
    }
}
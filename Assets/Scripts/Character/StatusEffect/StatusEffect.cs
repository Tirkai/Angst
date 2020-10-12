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
    public StatusEffect(List<IAttributeModifier> mods, bool isDurationable, float duration = 0)
    {
        modifiers = mods;
        Duration = duration;
        IsDurationable = isDurationable;
    }
}
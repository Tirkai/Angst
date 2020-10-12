using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SimpleAttributeModifier : IAttributeModifier
{
    SimpleModifierType Type { get; set; }
    float Amount { get; set; }

    public SimpleAttributeModifier(SimpleModifierType type, float amount)
    {
        Type = type;
        Amount = amount;
    }

}
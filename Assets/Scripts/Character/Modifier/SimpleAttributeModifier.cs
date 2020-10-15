using Character.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SimpleAttributeModifier : IAttributeModifier
{
    public ScalableAttributeType AttributeType { get; set; }
    public SimpleModifierType Type { get; set; }
    public float Amount { get; set; }

    public SimpleAttributeModifier(ScalableAttributeType attributeType, SimpleModifierType type, float amount)
    {
        AttributeType = attributeType;
        Type = type;
        Amount = amount;
    }

    public float Calculate(CharacterAttribute attribute)
    {
        switch (Type)
        {
            case SimpleModifierType.Add:
                return attribute.BaseAmount + Amount;
            case SimpleModifierType.Increase:
                return attribute.BaseAmount * Amount;
            case SimpleModifierType.Set:
                return Amount;
        }
        return 0;
    }

}
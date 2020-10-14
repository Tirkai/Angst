using Character.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ScaleAttributeModifier : IAttributeModifier
{
    public ScalableAttributeType SourceType { get; set; }
    public ScalableAttributeType RelatedType { get; set; }
    public float ScaleFactor { get; set; }
    public bool IsConvert { get; set;}

    public ScaleAttributeModifier(ScalableAttributeType sourceType, ScalableAttributeType relatedType, float scaleFactor, bool isConvert = false)
    {
        SourceType = sourceType;
        RelatedType = relatedType;
        ScaleFactor = scaleFactor;
        IsConvert = isConvert;
    }

}
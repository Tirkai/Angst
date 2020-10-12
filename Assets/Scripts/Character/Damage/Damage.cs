using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Damage
{
    DamageType type;
    float amount;

    public float Amount { get => amount; }

    public Damage(DamageType type, float amount)
    {
        this.type = type;
        this.amount = amount;
    }
}
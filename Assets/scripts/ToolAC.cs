using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ToolAC : ReseteableObjectAC
{
    public float staminaDrain = 20f;
    public abstract void Use();

    public virtual void ResetUsing()
    {

    }
}

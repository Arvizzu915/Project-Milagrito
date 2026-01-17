using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ToolAC : MonoBehaviour
{
    public float staminaDrain = 20f;
    

    public abstract void Use();
}

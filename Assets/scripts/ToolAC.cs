using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ToolAC : MonoBehaviour
{

    
    public abstract void Use();
}

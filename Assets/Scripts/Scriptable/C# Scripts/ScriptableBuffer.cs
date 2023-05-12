using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buffer", menuName = "Buffer")]
public class ScriptableBuffer : ScriptableObject
{
    public enum TipoBuffer { Fire_Rate, Damage, Range }
    public TipoBuffer type;
    public float buffValue;
}


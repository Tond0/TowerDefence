using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Buffer : MonoBehaviour, IPlaceable
{
    public ScriptableBuffer BehaviourBuffer;
    public int orderInColumn;

    private void OnEnable()
    {
        transform.parent.TryGetComponent<ColumnManager>(out ColumnManager colonna);
        for(int i = 0; i < orderInColumn; i++)
        {
            if (colonna.torrete[i] == null) return;

            switch (BehaviourBuffer.type)
            {
                case ScriptableBuffer.TipoBuffer.Damage:
                    colonna.torrete[i].dannoApp += BehaviourBuffer.buffValue;
                    break;
                case ScriptableBuffer.TipoBuffer.Fire_Rate:
                    colonna.torrete[i].fire_rateApp -= BehaviourBuffer.buffValue;
                    break;
                case ScriptableBuffer.TipoBuffer.Range:
                    colonna.torrete[i].raggioApp += BehaviourBuffer.buffValue;
                    break;
            }
        }
    }
}

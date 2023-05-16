using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public enum StatusBuffer { Attiva, Disattiva }


public class Buffer
{
    readonly Transform transformObj;
    readonly ScriptableBuffer BehaviourBuffer;
    readonly int orderInColumn;

    public Buffer(Transform transformObj, ScriptableBuffer BehaviourBuffer, int posInColumn)
    {
        this.transformObj = transformObj;
        this.BehaviourBuffer = BehaviourBuffer;
        this.orderInColumn = posInColumn;
    }

    public void BuffDebuff(StatusBuffer status)
    {
        int buffOrDebuf = status == StatusBuffer.Attiva ? 1 : -1;

        transformObj.parent.TryGetComponent<ColumnManager>(out ColumnManager colonna);

        for (int i = 0; i < orderInColumn; i++)
        {

            if (colonna.torrete[i] == null) return;

            switch (BehaviourBuffer.type)
            {
                case ScriptableBuffer.TipoBuffer.Damage:
                    colonna.torrete[i].dannoApp += BehaviourBuffer.buffValue * buffOrDebuf;
                    break;
                case ScriptableBuffer.TipoBuffer.Fire_Rate:
                    colonna.torrete[i].fire_rateApp -= BehaviourBuffer.buffValue * buffOrDebuf;
                    break;
                case ScriptableBuffer.TipoBuffer.Range:
                    colonna.torrete[i].raggioApp += BehaviourBuffer.buffValue * buffOrDebuf;
                    break;
            }

        }
    }
}

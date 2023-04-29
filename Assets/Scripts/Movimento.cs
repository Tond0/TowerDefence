using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Movimento
{
    private Vector3[] destinazioni;
    private Transform transform_entity;

    private Sequence sequenza;
    public bool finito;
    public Movimento(Vector3[] destinazioni, Transform transform_entity)
    {
        this.destinazioni = destinazioni;
        this.transform_entity = transform_entity;

        sequenza = DOTween.Sequence();
    }

    public void ConfigurazionePercorso(float velocita)
    {
        sequenza.Append(transform_entity.DOPath(destinazioni, velocita, PathType.Linear, PathMode.Full3D, 1, Color.red).OnWaypointChange(GuardaProssimaDestinazione));
    }

    private void GuardaProssimaDestinazione(int waypoint)
    {
        if (waypoint + 1 < destinazioni.Length)
        {
            transform_entity.DOLookAt(destinazioni[waypoint], 0.2f);
        }
        else
        {
            finito = true;
        }
    }

    public void Muovi()
    {
        sequenza.PlayForward();
    }
}

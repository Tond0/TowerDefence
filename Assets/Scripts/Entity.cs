using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public float PV;
    [SerializeField] private float danno;

    [Header("Movimento")]
    [NonSerialized] public float durata_percorso;
    [NonSerialized] public Transform[] destinazioni;

    private Movimento movimento_entity;

    // Start is called before the first frame update
    void Start()
    {
        //movimento_entity = new Movimento(ConvertiDestinazioni(), transform);

        //movimento_entity.ConfigurazionePercorso(durata_percorso);
    }

    // Update is called once per frame
    void Update()
    {
        if (/*movimento_entity.finito ||*/ PV <= 0)
            Destroy(gameObject);
        


        //movimento_entity.Muovi();
    }

    //DoPath non accetta il transform quindi devo convertire in vector2
    private Vector3[] ConvertiDestinazioni()
    {
        Vector3[] destinazione_vet = new Vector3[destinazioni.Length];

        for(int i = 0; i < destinazioni.Length; i++)
        {
            destinazione_vet[i] = destinazioni[i].position;
        }

        return destinazione_vet;
    }
}

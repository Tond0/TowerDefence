using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerNemici : MonoBehaviour
{
    [Header("Nemico e Tempo")]
    [SerializeField] private GameObject nemico;
    [SerializeField] float tempo_spawn;
    [Header("Quantita")]
    [SerializeField] int quantita_minima;
    [SerializeField] int quantita_massima;
    [SerializeField] float tempo_pausa_spawn;
    private int quantita_appoggio = 0;
    [Header("Impostazioni nemico")]
    [SerializeField] private Transform[] destinazioni;
    [SerializeField] private float durata_percorso;

    // Start is called before the first frame update
    void Start()
    {
        int quantita = Random.Range(quantita_minima, quantita_massima);
        StartCoroutine(SpawnNemici(quantita));
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectsOfType<Entity>().Length == 0)
        {
            quantita_massima += 1;
            quantita_minima += 1;

            quantita_appoggio = 0;

            int quantita = Random.Range(quantita_minima, quantita_massima);
            StartCoroutine(SpawnNemici(quantita));
        }

    }

    IEnumerator SpawnNemici(int quantita)
    {
        while (quantita_appoggio <= quantita)
        {
            GameObject nemico_spawnato = Instantiate(nemico, transform.position, Quaternion.identity);
            nemico_spawnato.transform.SetParent(transform);

            Entity entity = nemico_spawnato.GetComponent<Entity>();
            entity.durata_percorso = durata_percorso;
            entity.destinazioni = destinazioni;

            quantita_appoggio++;
            yield return new WaitForSeconds(tempo_pausa_spawn);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(int i = 0; i < destinazioni.Length - 1; i++)
        {
            Gizmos.DrawLine(destinazioni[i].position, destinazioni[i+1].position);

        }
    }
}

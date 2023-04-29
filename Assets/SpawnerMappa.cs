using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMappa : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private int altezza;
    [SerializeField] private int larghezza;
    // Start is called before the first frame update
    void Start()
    {
        SpawnMappa();
    }

    private void SpawnMappa()
    {
        GameObject raccoglitore = new GameObject("Pavimento");
        for(int i = 0; i < larghezza; i++)
        {
            GameObject riga = new GameObject("Riga " + i);
            riga.transform.SetParent(raccoglitore.transform);
            for(int j = 0; j < altezza; j++)
            {
                GameObject tile_spawnato = Instantiate(tile, new Vector3(j, 0 ,i), Quaternion.identity);
                tile_spawnato.name = i + " : " + j;
                tile_spawnato.transform.SetParent(riga.transform);
            }
        }
    }
}

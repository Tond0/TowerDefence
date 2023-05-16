using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Stats")]
    [Tooltip("Quante volte deve essere toccato da un nemico?")]public int vita;

    private void OnTriggerEnter(Collider other)
    {
        vita -= 1;
    }

    private void Update()
    {
        if (vita > 0) return;

        GameManager.current.InvokeGameOver();
    }
}

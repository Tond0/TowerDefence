using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Action OnGameStart;
    public Action OnGameOver;

    [Header("GamePlay options")]
    //Per quanto tempo sta resistendo il giocatore?
    [NonSerialized] public float survivorTime;
    private bool startTime;



    public static GameManager current;

    void Awake()
    {
        #region Creazione istanza
        if(current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion
        

        OnGameStart += StartGame;
        OnGameOver+= StopGame;
    }

    private void OnDestroy()
    {
        OnGameStart -= StartGame;
        OnGameOver -= StopGame;
    }

    public void InvokeStartGame()
    {
        OnGameStart.Invoke();
    }

    public void InvokeGameOver() 
    {
        OnGameOver.Invoke();
    }

    #region Start & End game
    private void StartGame()
    {
        FindObjectOfType<SpawnerNemici>().enabled = true;
        startTime = true;
    }

    private void StopGame()
    {
        FindObjectOfType<SpawnerNemici>().enabled = false;

        Entity[] entities = FindObjectsOfType<Entity>();

        foreach (Entity nemico in entities)
        {
            Destroy(nemico.gameObject);
        }

        startTime = false;
    }
    #endregion

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SpawnerNemici>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Tiene conto per quanto tempo il giocatore sta resistendo
        survivorTime += startTime ? Time.deltaTime : 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager current;

    private void Awake()
    {
        #region Creazione istanza
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion


        #region SetUp menus
        MenuPrincipale.SetActive(true);
        MenuInGame.SetActive(false);
        MenuGameOver.SetActive(false);
        #endregion
    }

    [Header("Menu principale")]
    [SerializeField] private GameObject MenuPrincipale;
    [Header("Menu in game")]
    [SerializeField] private GameObject MenuInGame;
    [SerializeField] private TextMeshProUGUI tempo;
    [Header("Menu gameover")]
    [SerializeField] private GameObject MenuGameOver;
    [SerializeField] private TextMeshProUGUI tempoFinale;

    private void OnDestroy()
    {
        GameManager.current.OnGameStart -= StartGame;
        GameManager.current.OnGameOver += EndGame;
    }

    private void StartGame()
    {
        MenuPrincipale.SetActive(false);
        MenuInGame.SetActive(true);
    }

    private void EndGame()
    {
        MenuInGame.SetActive(false);
        MenuGameOver.SetActive(true);

        float minutes = Mathf.FloorToInt(GameManager.current.survivorTime / 60);
        float seconds = Mathf.FloorToInt(GameManager.current.survivorTime % 60);
        tempoFinale.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.current.OnGameStart += StartGame;
        GameManager.current.OnGameOver += EndGame;
    }

    // Update is called once per frame
    void Update()
    {
        float minutes = Mathf.FloorToInt(GameManager.current.survivorTime / 60);
        float seconds = Mathf.FloorToInt(GameManager.current.survivorTime % 60);
        tempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

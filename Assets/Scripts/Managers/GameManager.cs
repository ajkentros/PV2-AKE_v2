using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // declara propiedad pública y estática Instance en la clase GameManager (get = obtener el valor de Instance, private set = la propia clase puede cambiar el valor de Instance).
    public static GameManager Instance { get; private set; }

   


    // define variable entera score (score del juego)
    private int score;

    // garantiza que el patrón de diseño Singleton se instancie una sola vez en la escena.
    private void Awake()
    {
        /*
         si la propiedad Instance = nulla (no se ha creado una instancia previamente) => 
            establece Instance en la instancia actual del objeto
            el objeto se mantiene a través de las transiciones de escena
            carga el score con el valor máximo del singleton PlayerPref = 0 (por si es negativo, inferior a 0)
         sino (ya existe una instancia de este objeto) =>
            se destruye el objeto actual para asegurarse de que solo haya una instancia en la escena en cualquier momento
        */
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            score = Mathf.Max(PlayerPrefs.GetInt("Puntaje"), 100);
            //score = 100;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // suscribe a evento
    private void OnEnable()
    {
        // asocia suscribiendo (=) al evento OnPause con el método Pausar
        // asocia suscribiendo (=) al evento OnResume con el método Reanudar
        GameEvents.OnPause += Pausar;
        GameEvents.OnResume += Reanudar;
        GameEvents.OnVictory += ManageOnVictory;
        GameEvents.OnGameOver += ManageOnGameOver;
    }

    // desuscribe a evento
    private void OnDisable()
    {
        // asocia desuscribiendo (-=) al evento OnPause con el método Pausar
        // asocia desuscribiendo (-=) al evento OnResume con el método Reanudar
        GameEvents.OnPause -= Pausar;
        GameEvents.OnResume -= Reanudar;
        GameEvents.OnVictory -= ManageOnVictory;
        GameEvents.OnGameOver -= ManageOnGameOver;
    }

    // pausa el juego
    private void Pausar()
    {
        // se congela el tiempo de juego al hacer que la escala de tiempo en la que ocurren los eventos en el juego = 0
        Time.timeScale = 0;
        Debug.Log("PAUSADO");
    }

    // reanuda el juego
    private void Reanudar()
    {
        // se congela el tiempo de juego al hacer que la escala de tiempo en la que ocurren los eventos en el juego = 1
        Time.timeScale = 1;
        Debug.Log("REANUDADO");
    }

    // dispara el trigger del evento
    private void Update()
    {
        /*
         si clic en la tecla Escape =>
            si la escala de tiempo en la que ocurren los eventos en el juego no es = 0 =>
                se dispara el evento global TriggerPause (pausa el juego)
            sino 
                se dispara el evento global TriggerResume (reactiva el juego)
         */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                GameEvents.TriggerPause();
            }
            else
            {
                GameEvents.TriggerResume();
            }
        }
    }

    // adiciona puntos a score
    public void AddScore(int points)
    {
        score += points;

        // solo para prueba de escenas
        //if(score <=0)
        //{
        //    SceneManager.LoadScene(0);
        //    ApplicationManager.Instance.GoToPreviousScene();
        //}

        //versión 1
        if (score < 0) score = 0;
        PlayerPrefs.SetInt("Puntaje", score);

        
    }

    // restaura el score
    public void ResetScore()
    {
        score = 0;
    }

    // lee el score
    public int GetScore()
    {
        return score;
    }

    // manejaa la victoria
    private void ManageOnVictory()
    {
        Time.timeScale = 0;
        ResetScore();
        Debug.Log("VICTORIA");
    }

    // maneja la derrota
    private void ManageOnGameOver()
    {
        Time.timeScale = 0; 
        ResetScore();
        Debug.Log("DERROTA");
    }
}

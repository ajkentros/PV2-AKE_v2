using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    // inicializa texto en UI
    [SerializeField] TextMeshProUGUI miTexto;

    // inicializa texto en UI
    [SerializeField] TextMeshProUGUI miMensaje;

    // inicializa el bot�n Volver
    [SerializeField] Button botonVolver;

    // inicializa GameObject �cono de vida
    [SerializeField] GameObject iconoVida;
    
    // inicializa GameObject contenedor de iconos de vida
    [SerializeField] GameObject contenedorIconosVida;

    // inicializa GameObject menu
    [SerializeField] GameObject menuConfig;


 

    // suscribe a evento
    private void OnEnable()
    {
        // asocia suscribiendo (=) al evento OnPause con el m�todo Pausar
        // asocia suscribiendo (=) al evento OnResume con el m�todo Reanudar
        GameEvents.OnPause += Pausar;
        GameEvents.OnResume += Reanudar;
        
        GameEvents.OnVictory += MostrarGanaste;
        GameEvents.OnGameOver += MostrarPerdiste;

        // suscribe el m�todo al evento de clic del bot�n "Volver"
        botonVolver.onClick.AddListener(VolverAPortada);
    }

    // desuscribe a evento
    private void OnDisable()
    {
        // asocia desuscribiendo (-=) al evento OnPause con el m�todo Pausar
        // asocia desuscribiendo (-=) al evento OnResume con el m�todo Reanudar
        GameEvents.OnPause -= Pausar;
        GameEvents.OnResume -= Reanudar;

        GameEvents.OnVictory -= MostrarGanaste;
        GameEvents.OnGameOver -= MostrarPerdiste;

        // desuscribe el m�todo del evento de clic del bot�n "Volver"
        botonVolver.onClick.RemoveListener(VolverAPortada);

    }

    // pausa el juego, actualiza el texto y el men�
    private void Pausar()
    {
        ActualizarTextoHUD("PAUSADO");
        menuConfig.SetActive(true);
    }

    // reanuda el juego, actualiza el texto y el men�
    private void Reanudar()
    {
        ActualizarTextoHUD(GameManager.Instance.GetScore().ToString());
        menuConfig.SetActive(false);
    }


    // Actualiza el texto en UI
    public void ActualizarTextoHUD(string nuevoTexto)
    {
        // muestra en consola la cantidad de vida
        // asigna a miTexto la cantidad de vida
        Debug.Log("SE LLAMA " + nuevoTexto);
        miTexto.text = nuevoTexto;
    }

    public void ActualizarVidasHUD(int vidas)
    {
        // nuestra en consola mensake
        // si el contenedor est� vac�o =>
        //  carga el contenedor y retorna
        
        if (EstaVacioContenedor())
        {
            Debug.Log("HUDController.cs ESTAS ACTUALIZANDO VIDAS " + vidas);
            CargarContenedor(vidas);
            return;
        }

        // si vidas > 0 =>
        //  elimina el �ltimo �cono
        //  sino => crea un �cono
        if (CantidadIconosVidas() > vidas)
        {
            EliminarUltimoIcono();
        }
        else
        {
            CrearIcono();
        }
    }

    // Retorna verdadero si la cantidad de iconos es = 0
    private bool EstaVacioContenedor()
    {
        return contenedorIconosVida.transform.childCount == 0;
    }

    // Retorna la cantidad de iconos de vida
    private int CantidadIconosVidas()
    {
        return contenedorIconosVida.transform.childCount;
    }

    // elimina el �ltimo GameObject que est� en el contenedor
    private void EliminarUltimoIcono()
    {
        Transform contenedor = contenedorIconosVida.transform;
        GameObject.Destroy(contenedor.GetChild(contenedor.childCount - 1).gameObject);
    }

    // Carga el contenedor con la cantidad de vidas
    private void CargarContenedor(int cantidadIconos)
    {
        // Crea un �cono de vida seg�n la cantidad de �conos de vida
        for (int i = 0; i < cantidadIconos; i++)
        {
            CrearIcono();
        }
    }

    // Crea el �cono de vida
    private void CrearIcono()
    {
        Instantiate(iconoVida, contenedorIconosVida.transform);
    }

    private void MostrarGanaste()
    {
        
        miMensaje.text = "GANASTE";
        miMensaje.color = Color.green; // Puedes cambiar el color seg�n tus preferencias
      
        // activa el bot�n "Volver"
        BotonVolver();

    }

    private void MostrarPerdiste()
    {
        
        miMensaje.text = "PERDISTE";
        miMensaje.color = Color.red; // Puedes cambiar el color seg�n tus preferencias

        // activa el bot�n "Volver"
        BotonVolver();

    }

    // gestiona el bot�n Volver
    private void BotonVolver()
    {
        botonVolver.gameObject.SetActive(true);
    }

    // gestiona el regreso a la portada
    private void VolverAPortada()
    {
        // 
        SceneManager.LoadScene("Portada");
    }
}

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

    // inicializa el botón Volver
    [SerializeField] Button botonVolver;

    // inicializa GameObject ícono de vida
    [SerializeField] GameObject iconoVida;
    
    // inicializa GameObject contenedor de iconos de vida
    [SerializeField] GameObject contenedorIconosVida;

    // inicializa GameObject menu
    [SerializeField] GameObject menuConfig;


 

    // suscribe a evento
    private void OnEnable()
    {
        // asocia suscribiendo (=) al evento OnPause con el método Pausar
        // asocia suscribiendo (=) al evento OnResume con el método Reanudar
        GameEvents.OnPause += Pausar;
        GameEvents.OnResume += Reanudar;
        
        GameEvents.OnVictory += MostrarGanaste;
        GameEvents.OnGameOver += MostrarPerdiste;

        // suscribe el método al evento de clic del botón "Volver"
        botonVolver.onClick.AddListener(VolverAPortada);
    }

    // desuscribe a evento
    private void OnDisable()
    {
        // asocia desuscribiendo (-=) al evento OnPause con el método Pausar
        // asocia desuscribiendo (-=) al evento OnResume con el método Reanudar
        GameEvents.OnPause -= Pausar;
        GameEvents.OnResume -= Reanudar;

        GameEvents.OnVictory -= MostrarGanaste;
        GameEvents.OnGameOver -= MostrarPerdiste;

        // desuscribe el método del evento de clic del botón "Volver"
        botonVolver.onClick.RemoveListener(VolverAPortada);

    }

    // pausa el juego, actualiza el texto y el menú
    private void Pausar()
    {
        ActualizarTextoHUD("PAUSADO");
        menuConfig.SetActive(true);
    }

    // reanuda el juego, actualiza el texto y el menú
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
        // si el contenedor está vacío =>
        //  carga el contenedor y retorna
        
        if (EstaVacioContenedor())
        {
            Debug.Log("HUDController.cs ESTAS ACTUALIZANDO VIDAS " + vidas);
            CargarContenedor(vidas);
            return;
        }

        // si vidas > 0 =>
        //  elimina el último ícono
        //  sino => crea un ícono
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

    // elimina el último GameObject que está en el contenedor
    private void EliminarUltimoIcono()
    {
        Transform contenedor = contenedorIconosVida.transform;
        GameObject.Destroy(contenedor.GetChild(contenedor.childCount - 1).gameObject);
    }

    // Carga el contenedor con la cantidad de vidas
    private void CargarContenedor(int cantidadIconos)
    {
        // Crea un ícono de vida según la cantidad de íconos de vida
        for (int i = 0; i < cantidadIconos; i++)
        {
            CrearIcono();
        }
    }

    // Crea el ícono de vida
    private void CrearIcono()
    {
        Instantiate(iconoVida, contenedorIconosVida.transform);
    }

    private void MostrarGanaste()
    {
        
        miMensaje.text = "GANASTE";
        miMensaje.color = Color.green; // Puedes cambiar el color según tus preferencias
      
        // activa el botón "Volver"
        BotonVolver();

    }

    private void MostrarPerdiste()
    {
        
        miMensaje.text = "PERDISTE";
        miMensaje.color = Color.red; // Puedes cambiar el color según tus preferencias

        // activa el botón "Volver"
        BotonVolver();

    }

    // gestiona el botón Volver
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

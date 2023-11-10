using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] gameObjectsToActivate;
    public GameObject[] gameObjectsToDeactivate;

    // se llama cuando se haga clic en el bot�n Salir.
    public void OnExitButtonClicked()
    {
        // Activa los objetos que se desearon al salir del men�.
        foreach (GameObject obj in gameObjectsToActivate)
        {
            obj.SetActive(true);
        }

        // Desactiva el objeto Men�.
        gameObject.SetActive(false);

        // Desactiva los objetos que se desearon al salir del men�.
        foreach (GameObject obj in gameObjectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }
}

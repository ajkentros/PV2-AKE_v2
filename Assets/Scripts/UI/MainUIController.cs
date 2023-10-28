using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    // carga la escena la escena siguiente usando el Singleton ApplicationManager
    public void CargarSiguienteEscena()
    {
        ApplicationManager.Instance.GoToNextScene();
    }
}

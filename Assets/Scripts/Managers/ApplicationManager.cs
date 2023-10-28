using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    // declara propiedad pública y estática Instance en la clase ApplicationManager (get = obtener el valor de Instance, private set = la propia clase puede cambiar el valor de Instance).
    public static ApplicationManager Instance { get; private set; }

    void Awake()
    {
        /*
         si la propiedad Instance = nulla (no se ha creado una instancia previamente) => 
            establece Instance en la instancia actual del objeto
            el objeto se mantiene a través de las transiciones de escena
         sino (ya existe una instancia de este objeto) =>
            se destruye el objeto actual para asegurarse de que solo haya una instancia en la escena en cualquier momento
        */
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // va a la escena siguiente
    public void GoToNextScene()
    {
        /*
        define variable int currentSceneIndex = obtiene el índice de la escena activa actual
        define variable int nextSceneIndex (índice de la siguiente escena) = currentSceneIndex + 1
        carga la escena nextSceneIndex
        */
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        GoToScene(nextSceneIndex);
    }

    // va a la escena previa
    public void GoToPreviousScene()
    {
        /*
        define variable int currentSceneIndex = obtiene el índice de la escena activa actual
        define variable int previousSceneIndex (índice de la siguiente escena) = currentSceneIndex - 1
        carga la escena
        */
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int previousSceneIndex = currentSceneIndex - 1;

        GoToScene(previousSceneIndex);
    }

    // carga la escena según el índice 
    private void GoToScene(int sceneIndex)
    {
        /*
         si la escena previa >= 0 =>
            carga la escena previousSceneIndex
        sino =>
            mensaje en consola
        */
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("No hay más escenas después de la actual");
        }

    }
}

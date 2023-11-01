using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    // declara propiedad pública y estática Instance en la clase PesistenceManager (get = obtener el valor de Instance, private set = la propia clase puede cambiar el valor de Instance).
    public static PersistenceManager Instance { get; private set; }

    // define métodos públicos que devuelven string para cada acceder a la clave correspondiente
    public static string KeyMusic { get => Instance.keyMusic; }
    public static string KeyVolume { get => Instance.keyVolume; }
    public static string KeyUser { get => Instance.keyUser; }
    public static string KeyScore { get => Instance.keyScore; }


    // define variables privadas del tipo string: keyMusic, keyVolume, keyUser, keyScore
    [SerializeField] private string keyMusic, keyVolume, keyUser, keyScore;

    private void Awake()
    {
        /*
         si la propiedad Instance = nulla (no se ha creado una instancia previamente) => 
            establece Instance en la instancia actual del objeto
            el objeto se mantiene a través de las transiciones de escena
            score = 1000 (valor de inicial)
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

    // setea valores enteros
    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    // toma valores enteros
    public int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    // setea valores float
    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    // toma valores float
    public float GetFloat(string key, float defaultValue = 0.0f)
    {
        return PlayerPrefs.GetFloat(key, defaultValue);
    }

    // setea valores string
    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    // toma valores string
    public string GetString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    // setea valor bool
    public void SetBool(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }

    // toma valores bool
    public bool GetBool(string key, bool defaultValue = false)
    {
        int value = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0);
        return value == 1;
    }

    // guarda los datos
    public void Save()
    {
        PlayerPrefs.Save();
    }

    // borra la clave
    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    // borra todas las claves
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    // guarda la configuración de la música
    public void SaveMusicConfig(bool status)
    {
        SetBool(keyMusic, status);
        Debug.Log(" El jugador presiono " + status);
    }

    // guarda la configuración del volumen
    public void SaveVolumenConfig(float volume)
    {
        SetFloat(keyVolume, volume);
        Debug.Log(" Volumen Escogido " + volume);
    }

    // guarda el nombre del usuario
    public void SaveUserName(string value)
    {
        SetString(keyUser, value);
        Debug.Log(" El nombre es " + value);
    }

}

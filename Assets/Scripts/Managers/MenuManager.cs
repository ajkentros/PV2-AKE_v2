using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // define variables privadas y serializadas del tipo Slider: mySlider, Toggle: myToggle, TPM_InputField: myInput
    [SerializeField] private Slider mySlider;
    [SerializeField] private Toggle myToggle;
    [SerializeField] private TMP_InputField myInput;

    void Start()
    {
        // asigna valores iniciales con las instancias iniciales de cada clave obtenidas del Singleton PersistenceManager
        mySlider.value = PersistenceManager.Instance.GetFloat(PersistenceManager.KeyVolume);
        myToggle.isOn = PersistenceManager.Instance.GetBool(PersistenceManager.KeyMusic);
        myInput.text = PersistenceManager.Instance.GetString(PersistenceManager.KeyUser);
    }

    private void OnDisable()
    {
        PersistenceManager.Instance.Save();
    }

    private void OnEnable()
    {
        if (PersistenceManager.Instance != null)
        {
            PersistenceManager.Instance.SetInt(PersistenceManager.KeyScore, GameManager.Instance.GetScore());
        }
    }
}

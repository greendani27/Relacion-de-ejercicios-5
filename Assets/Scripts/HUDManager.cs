using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] public TMP_Text puntos;
    [SerializeField] private TMP_Text textTimer;

    private void Start()
    {
        GameManager.Instance.HUD = this;
        /*if (SceneManager.GetActiveScene().name.Equals("FinalScene")) {
            Debug.Log(PlayerPrefs.GetInt("finalPoints"));
            
        }*/
        
    }

    public void MostrarPuntos(int enemys)
    {
        puntos.text = enemys.ToString();
    }

    public void UpdateTimer() {
        textTimer.text = ((int)GameManager.Instance.timer).ToString();
    }

    public int getPuntos() {
        return int.Parse(puntos.text);
    }
}

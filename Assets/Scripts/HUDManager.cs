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

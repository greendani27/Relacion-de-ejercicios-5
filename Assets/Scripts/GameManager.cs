using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    public int enemys = 0;
    
    public HUDManager HUD;
    public static GameManager Instance { get; private set; }

    [HideInInspector] public int vidas = 2;

    [SerializeField] public Sprite[] spriteVidas;
    public SpriteRenderer contadorVidas;

    public float timer;
    public float finalTime;

    public float endScreenTimer;
    [HideInInspector] public AudioSource explosion;

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level01"))
        {
            if (HUD.getPuntos() == 27)
            {
                PlayerPrefs.SetInt("finalPoints", HUD.getPuntos());
                PlayerPrefs.SetInt("finalTime", (int) timer);
                if ((PlayerPrefs.GetInt("finalPoints") + PlayerPrefs.GetInt("finalTime")) > PlayerPrefs.GetInt("maxPuntuacion"))
                {
                    PlayerPrefs.SetInt("maxPuntuacion", PlayerPrefs.GetInt("finalPoints") + PlayerPrefs.GetInt("finalTime"));
                }
                ChangeScene("FinalScene");
            }
            else if (timer < 0 || vidas < 0) {
                PlayerPrefs.SetInt("finalPoints", HUD.getPuntos());
                Debug.Log(PlayerPrefs.GetInt("finalPoints"));
                PlayerPrefs.SetInt("finalTime", 0);

                if ((PlayerPrefs.GetInt("finalPoints") + PlayerPrefs.GetInt("finalTime")) > PlayerPrefs.GetInt("maxPuntuacion"))
                {
                    PlayerPrefs.SetInt("maxPuntuacion", PlayerPrefs.GetInt("finalPoints") + PlayerPrefs.GetInt("finalTime"));
                }
                ChangeScene("FinalScene");
            }
            else
            {
                timer -= Time.deltaTime;
                HUD.UpdateTimer();
            }
        }
        if (SceneManager.GetActiveScene().name.Equals("FinalScene")) {
            endScreenTimer -= Time.deltaTime;

            if (endScreenTimer < 0) {
                ChangeScene("InitialScene");
            } 
        }

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level01"))
        {
            explosion = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
            contadorVidas = GameObject.FindGameObjectWithTag("Lifes").GetComponent<SpriteRenderer>();
        }

        if (SceneManager.GetActiveScene().name.Equals("FinalScene"))
        {
            endScreenTimer = 10;
            GameObject.FindGameObjectWithTag("finalPoints").GetComponent<TMP_Text>().text = string.Format("Puntuacion: {0}", (PlayerPrefs.GetInt("finalPoints") + PlayerPrefs.GetInt("finalTime")));
        }

        if (SceneManager.GetActiveScene().name.Equals("InitialScene")) {
            GameObject.FindGameObjectWithTag("MaxPoints").GetComponent<TMP_Text>().text = string.Format("Puntuacion: {0}", PlayerPrefs.GetInt("maxPuntuacion"));
        }
    }

    public void AddEnemy()
    {
        enemys++;
        HUD.MostrarPuntos(enemys);
    }

    public void ChangeScene(string SceneName) { 
        SceneManager.LoadScene(SceneName);
    }

}

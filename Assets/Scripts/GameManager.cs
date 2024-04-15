using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseGeneral;
    public bool pause;
    public int life = 10;
    public Slider barLife;
    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        
    }
    public void GoGame()
    {
        SceneManager.LoadScene("Nivel1");
    }
    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Exit()
    {
        Debug.Log("Esta saliendo del juego");
    }
    public void Reanudar()
    {
        pauseGeneral.SetActive(false);
        Time.timeScale = 1;
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene("Nivel1");
    } 
    public void Pause()
    {
        pause = !pause;
        if (pause)
        {
            pauseGeneral.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseGeneral.SetActive(false);
            Time.timeScale = 1;
        }
    }
}


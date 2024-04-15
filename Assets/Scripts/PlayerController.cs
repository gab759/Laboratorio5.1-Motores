using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    public delegate void LifeUpdateDelegate(int newLife);
    public static event LifeUpdateDelegate OnLifeUpdated;
    public delegate void PointUpdateDelegate(int  newPoint);
    public static event PointUpdateDelegate OnPointUpdated;
    public static event Action OnPlayerDead;
    public static event Action OnPlayerWin;
    private CinemachineImpulseSource impulseSource;

    private float speed = 3f;
    public GameObject player;
    public float direction;
    public LayerMask layerMask;
    public bool suelo;
    public bool doubleJump;
    public int life = 10;  
    public int point = 0;
    public int playerColor;
    public Slider barLife;
    public TextMeshProUGUI textPoint;
    public float jumpForce = 4.5f;
    Rigidbody2D _compRigidbody2D;
    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void FixedUpdate()
    {
        _compRigidbody2D.velocity = new Vector2(direction * speed, _compRigidbody2D.velocity.y);
    }
    public void OnMovement(InputAction.CallbackContext contex)
    {
        direction = contex.ReadValue<float>();  
    }
    public void OnJump(InputAction.CallbackContext contex)
    {
        if (contex.phase == InputActionPhase.Performed) 
        {
            if (suelo || doubleJump)
            {
                if (!suelo)
                {
                    doubleJump = false;
                }
                _compRigidbody2D.velocity = new Vector2(_compRigidbody2D.velocity.x, jumpForce);
            }
        }
    }
    public void OnColorRed(InputAction.CallbackContext contex)
    {
          player.GetComponent<SpriteRenderer>().color = Color.red;
          playerColor = 1;
    }
    public void OnColorBlue(InputAction.CallbackContext contex)
    {
        player.GetComponent<SpriteRenderer>().color = Color.blue;
        playerColor = 2;
    }
    public void OnColorYellow(InputAction.CallbackContext contex)
    {
        player.GetComponent<SpriteRenderer>().color = Color.yellow;
        playerColor = 4;
    }

    private void Update()
    {
        Check();
    }
    public void DecreaseLife(int amount)
    {
        life -= amount;
        OnLifeUpdated?.Invoke(life);
        if (life <= 0)
        {
            SceneManager.LoadScene("YouLoss");
        }
        CameraShake2.instance.CameraShake(impulseSource);
    }
    public void IncreaseLife(int amount)
    {
        life += amount;
        OnLifeUpdated?.Invoke(life);
    }
    public void IncreasePoint(int amount)
    {
        point += amount;
        Debug.Log("Puntos sumados: " + point);
        OnPointUpdated?.Invoke(point);
    }
    public void Rojo()
    {
        
    }
    public void Blue()
    {
        
    }
    public void Green()
    {   
        player.GetComponent<SpriteRenderer>().color = Color.green;
        playerColor = 3;
    }
    public void Yellow()
    {
        
    }
    public void Cyan()
    {
        player.GetComponent<SpriteRenderer>().color = Color.cyan;
        playerColor = 5;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Dispara el evento perder
        if (collision.CompareTag("Muerte"))
        {
            OnPlayerDead?.Invoke();
            SceneManager.LoadScene("YouLoss");
        }
        if (collision.CompareTag("puerta"))
        {
            SceneManager.LoadScene("Nivel2");
        }
        //Dispara el evento ganar
        if (collision.CompareTag("win"))
        {
            OnPlayerWin?.Invoke();
            SceneManager.LoadScene("YouWin");
        }
        if (collision.tag == "Red")
        {
            if (playerColor == 1)
            { }
            else
            {
                DecreaseLife(2);
                Debug.Log("vida restada: " + life);
                barLife.value = life;    
            }
        }
        if (collision.tag == "Blue")
        {
            if (playerColor == 2)
            { }
            else
            {
                DecreaseLife(2);
                Debug.Log("vida restada: " + life);
                barLife.value = life;
            }
        }
        if (collision.tag == "Green")
        {
            if (playerColor == 3)
            { }
            else
            {
                DecreaseLife(2);
                Debug.Log("vida restada: " + life);
                barLife.value = life;

            }
        }
        if (collision.tag == "Yellow")
        {
            if (playerColor == 4)
            { }
            else
            {
                DecreaseLife(2);
                Debug.Log("vida restada: " + life);
                barLife.value = life;

            }
        }
        if (collision.tag == "Cyan")
        {
            if (playerColor == 5)
            { }
            else
            {
                DecreaseLife(2);
                Debug.Log("vida restada: " + life);
                barLife.value = life;
            }
        }
        if(collision.tag == "Life")
        {
            IncreaseLife(2);
            barLife.value = life;
        }
        if (collision.tag == "Point")
        {
            IncreasePoint(10);
        }
        if (collision.CompareTag("Life"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Point"))
        {
            Destroy(collision.gameObject);
        }

    }
    private void Check()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.blue);
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMask))
        {
            suelo = true;
            doubleJump = true;
        }
        else
        {
            suelo = false;
        }
        //SI LA VIDA ES MENOR A 0 PIERDES
        if (life <= 0)
        {
            SceneManager.LoadScene("YouLoss");
        }
        textPoint.text = "Puntaje: " + point;
    }

}

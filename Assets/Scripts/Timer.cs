using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    public float time = 0;
    public TextMeshProUGUI textoTime;
    private void Update()
    {
        time = time + Time.deltaTime;

        textoTime.text = "" + time.ToString("f0");
    }
}

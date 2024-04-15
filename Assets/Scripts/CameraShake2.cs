using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake2 : MonoBehaviour
{
    public static CameraShake2 instance;
    [SerializeField] private float shakeForce = 1f;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }
}

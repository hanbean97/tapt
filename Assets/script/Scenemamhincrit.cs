using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Scenemamhincrit : Singltons<Scenemamhincrit>
{
    private CinemachineVirtualCamera cmv;
    private CinemachineBasicMultiChannelPerlin channelperin;
    [SerializeField]
    private float frequency = 1f;
    private float countdown = 0f;
    float ShakeTime= 0f;
    private bool isshake = false;
    [SerializeField] float baseAmplitudeGain;
    [SerializeField]float baseFrequencyGain;
    protected override void Awake()
    {
        base.Awake();

        cmv= this.GetComponent<CinemachineVirtualCamera>();
        channelperin = cmv.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelperin.m_AmplitudeGain = baseAmplitudeGain;
        channelperin.m_FrequencyGain = baseFrequencyGain;
    }
    
    private void Update()
    {
        if (isshake == true)
        {
            countdown += Time.deltaTime;
            if (countdown >= ShakeTime)
            {
                stoptShake();

                countdown = 0f;
            }
        }
       
    }
    public void StartShake(float intencity, float sTime)
    {
        channelperin.m_AmplitudeGain = intencity;//Æø
        channelperin.m_FrequencyGain = frequency ;//¼Óµµ

        isshake = true; 
        ShakeTime = sTime;
    }
    void stoptShake()
    {
        channelperin.m_AmplitudeGain = baseAmplitudeGain;
        channelperin.m_FrequencyGain = baseFrequencyGain;
        isshake = false;
    }
}

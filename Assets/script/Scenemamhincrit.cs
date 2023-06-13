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
    protected override void Awake()
    {
        base.Awake();

        cmv= this.GetComponent<CinemachineVirtualCamera>();
        channelperin = cmv.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartShake(0.5f,1f);
        }
    }
    public void StartShake(float intencity, float sTime)
    {
        channelperin.m_AmplitudeGain = intencity;//Æø
        channelperin.m_FrequencyGain = frequency;//¼Óµµ

        isshake = true;
        ShakeTime = sTime;
    }
    void stoptShake()
    {
        channelperin.m_FrequencyGain = 0;
        channelperin.m_AmplitudeGain = 0;
        isshake = false;
    }
}

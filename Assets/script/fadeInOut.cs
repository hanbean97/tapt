using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeInOut : MonoBehaviour
{
    private Image fade;
    float fadetime;
    public AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        fade = GetComponent<Image>();
        StartCoroutine(fadeinCor());//코루틴은 한번만 실행되어야함 업더이트에서는 한번만실행되게 바꿔줘야함

    }

    // Update is called once per frame
    void Update()
    {

    }

    void startfad()
    {
        if (fadetime > 1)
        {
            return;
        }
        fadetime += 0.1f * Time.deltaTime;
        fade.color = new Color(0, 0, 0, (1 - fadetime) / 1);

    }

    IEnumerator fadeinCor()
    {
        float a = 1;
        while (a >= 0f)
        {
            a -= Time.deltaTime;
            float cvlaue = curve.Evaluate(a);
            fade.color = new Color(0, 0, 0, cvlaue);
            yield return 0;// 일단 멈춤 밑에 코드 실행하고 다시 돌아와서 다시시작
        }
    }
    IEnumerator fadeOutCor(int scenenum)
    {
        float a = 0;
        while (a <= 1f)
        {
            a += Time.deltaTime;
            float cvlaue = curve.Evaluate(a);
            fade.color = new Color(0, 0, 0, cvlaue);
            yield return 0;// 일단 멈춤 다시 돌아와서 다시시작
        }

        SceneManager.LoadScene(scenenum);// 밖에 쓰면 돌때마다 실행 여기는 while문이 끝나고 실행
    }
    public void fadoutScene(int scenenum)
    {
        StartCoroutine(fadeOutCor(scenenum));
        Time.timeScale = 1;
    }


}

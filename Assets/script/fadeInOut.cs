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
        StartCoroutine(fadeinCor());//�ڷ�ƾ�� �ѹ��� ����Ǿ���� ������Ʈ������ �ѹ�������ǰ� �ٲ������

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
            yield return 0;// �ϴ� ���� �ؿ� �ڵ� �����ϰ� �ٽ� ���ƿͼ� �ٽý���
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
            yield return 0;// �ϴ� ���� �ٽ� ���ƿͼ� �ٽý���
        }

        SceneManager.LoadScene(scenenum);// �ۿ� ���� �������� ���� ����� while���� ������ ����
    }
    public void fadoutScene(int scenenum)
    {
        StartCoroutine(fadeOutCor(scenenum));
        Time.timeScale = 1;
    }


}

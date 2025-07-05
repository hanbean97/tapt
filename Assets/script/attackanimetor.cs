using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackanimetor : MonoBehaviour
{
    [SerializeField] Animator attackani;
    public void goattack()
    {
        attackani.SetTrigger("attack");
    }
}

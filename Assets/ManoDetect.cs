using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoDetect : MonoBehaviour,ITakeDamage
{
    public IAMano iAMano;
    public void TakeDamage(int damage)
    {
        iAMano.TakeDamage(damage);
    }
}

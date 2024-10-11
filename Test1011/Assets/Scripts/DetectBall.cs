using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BasketBall basket = other.GetComponent<BasketBall>();

        if(basket != null)
        {
            UIManager._Inst.GoalIn();
        }
    }
}

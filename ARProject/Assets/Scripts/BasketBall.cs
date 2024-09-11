using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    private void Update()
    {
        CheckAlive();   
    }

    void CheckAlive()
    {
        Vector3 worldPos = transform.position;

        Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);

        if(pos.y < -30)
        {
            Destroy(gameObject);
        }
    }
}

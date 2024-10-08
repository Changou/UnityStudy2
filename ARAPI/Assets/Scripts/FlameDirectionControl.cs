using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameDirectionControl : MonoBehaviour
{
    public void SetDir(int num)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            if(i == num)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}

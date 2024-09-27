using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Accessories : MonoBehaviour
{
    public enum TYPE
    {
        MASK1,
        MASK2
    }

    [SerializeField] public TYPE _type;
}

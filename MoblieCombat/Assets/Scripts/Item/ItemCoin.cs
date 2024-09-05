using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : ItemBase
{
    [SerializeField] float _rotSpeed;

    protected override void OnEffect(Collider other)
    {
        GameManager._Inst.CoinUp(10);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, _rotSpeed *Time.deltaTime, 0),Space.World);  
    }
}

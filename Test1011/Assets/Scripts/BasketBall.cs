using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    Rigidbody _rb;

    [SerializeField] float _checkBallOutScreenDist;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void ThrowingBall(Vector3 dir, float power)
    {
        _rb.isKinematic = false;
        transform.SetParent(null);
        _rb.AddRelativeForce(dir * power, ForceMode.Impulse);
    }

    private void Update()
    {
        CheckAlive();
    }

    void CheckAlive()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        if (pos.x < -_checkBallOutScreenDist || pos.x > Screen.width + _checkBallOutScreenDist
            || pos.y < -_checkBallOutScreenDist || pos.y > Screen.height + _checkBallOutScreenDist
            || transform.position.z > Camera.main.farClipPlane + 5)
            Destroy(gameObject);
    }
}

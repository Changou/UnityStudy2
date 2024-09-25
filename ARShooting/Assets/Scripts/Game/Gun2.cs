using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2 : Gun
{
    [Header("ÃÑ ¹ß»ç")]
    [SerializeField] public Transform _shotPos;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(
                touch.position.x,touch.position.y, Camera.main.nearClipPlane * 4));

            Ray ray = new Ray(touchPos, Camera.main.transform.forward);
            Debug.DrawRay(touchPos, Vector3.forward);

            RaycastHit hitInfo;

            int layerMask = 1 << LayerMask.NameToLayer("Gun");

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
            {
                Fire();
            }
        }
    }

    [Header("ÃÑ ¼º´É")]
    [SerializeField] public int _pow;
    [SerializeField] public float _shotDelay;
    [SerializeField] public int _magazine;
    [SerializeField] public float _reload;
    [SerializeField] public int _bulletCnt;
    [SerializeField][Range(1f,5f)] public float _gunRange;
    [SerializeField] public LineRenderer _linePrefab;
    public bool _isShot = true;

    protected virtual void Fire() {}
}

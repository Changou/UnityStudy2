using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : MonoBehaviour, IEntity
{
    float _speed = 0.5f;

    Rigidbody _rb;
    Animator _anim;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    [Header("파동빈도"), SerializeField] 
    [Range(1f, 10f)] float frequency = 2f;

    [Header("파동높이"), SerializeField]
    [Range(0.005f, 0.01f)] float waveHeight = 0.005f;

    bool _isAlive = true;

    enum MOVESTYLE
    {
        MOVE,
        WAVE,
        HOOP,

        MAX
    }

    [Header("새 움직임 스타일"), SerializeField] MOVESTYLE _style;

    void Start()
    {
        _isAlive = true;

        int ran = Random.Range(0, (int)MOVESTYLE.MAX);

        _style = (MOVESTYLE)ran;

        if( _style == MOVESTYLE.HOOP )
            Hoop();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive)
        {
            if (_style == MOVESTYLE.MOVE)
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
            }
            else if (_style == MOVESTYLE.WAVE)
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
                transform.position += transform.up * Mathf.Sin(Time.time * frequency) * waveHeight;
            }
            else if (_style == MOVESTYLE.HOOP)
            {
                HoopMove();
            }
        }
        CheckAlive();
    }

    float _rotSpeed = 200f;

    void HoopMove()
    {
        transform.parent.parent.position += transform.parent.parent.forward * _speed * Time.deltaTime;
        transform.parent.Rotate(new Vector3(0, 0, _rotSpeed * Time.deltaTime));

        if (transform.rotation.y > 0)
        {
            transform.Rotate(new Vector3(_rotSpeed * Time.deltaTime, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(-_rotSpeed * Time.deltaTime, 0, 0));
        }
    }

    void Hoop()
    {
        GameObject hoop = new GameObject();
        GameObject hoopParent = new GameObject();
        hoop.transform.position = transform.position;
        hoopParent.transform.position = transform.position;
        transform.SetParent(hoop.transform);

        Vector3 pos = transform.localPosition;
        if(transform.rotation.y < 0)
        {
            pos.x -= 0.3f;
            
        }
        else
        {
            pos.x += 0.3f;

        }
        hoopParent.transform.rotation = transform.rotation;
        hoop.transform.SetParent(hoopParent.transform);
        transform.localPosition = pos;
    }

    void CheckAlive()
    {
        if (transform.position.x < -3 || transform.position.x > 3 || transform.position.y < -3)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void Catch(Vector3 pos)
    {
        GameManager._Inst.CatchBird();
        _anim.SetTrigger("Die");
        _rb.useGravity = true;
        _rb.AddForce(Vector3.up * 4f, ForceMode.Impulse);
        _isAlive = false;
    }
}

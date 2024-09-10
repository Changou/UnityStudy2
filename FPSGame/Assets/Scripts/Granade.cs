using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _effect;
    [SerializeField] float _power;
    [SerializeField] float _explosionDelay;

    private void OnEnable()
    {
        NewGranade();
    }

    private void OnDisable()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    public void Throw()
    {
        GameObject granade = transform.GetChild(0).gameObject;
        granade.transform.SetParent(null);

        granade.GetComponent<Rigidbody>().isKinematic = false;
        granade.GetComponent<Rigidbody>().AddForce((granade.transform.forward + granade.transform.up) * _power, ForceMode.Impulse);

        StartCoroutine(Explosion(granade));
    }

    IEnumerator Explosion(GameObject obj)
    {
        yield return new WaitForSeconds(_explosionDelay);
        GameObject effect = Instantiate(_effect, obj.transform);
        effect.transform.localPosition = Vector3.zero;
        effect.transform.SetParent(null);
        Destroy(obj);
        yield return new WaitForSeconds(1);
        NewGranade();
    }

    void NewGranade()
    {
        GameObject granade = Instantiate(_prefab, transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _veggiePrefabs;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _bombChance = 0.05f;

    [SerializeField] private float _minSpawnDelay = 0.25f;
    [SerializeField] private float _maxSpawnDelay = 1f;

    [SerializeField] private float _minAngle = -15f;
    [SerializeField] private float _maxAngle = 15f;

    [SerializeField] private float _minForce = 18f;
    [SerializeField] private float _maxForce = 22f;

    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1.5f);

        while(enabled)
        {
            GameObject veggie = _veggiePrefabs[Random.Range(0, _veggiePrefabs.Length)];

            if (Random.value < _bombChance)
                veggie = _bombPrefab;

            Vector3 pos = new Vector3();
            pos.x = Random.Range(_collider.bounds.min.x, _collider.bounds.max.x);
            pos.y = _collider.bounds.max.y;
            pos.z = _collider.bounds.max.z;

            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(_minAngle, _maxAngle));

            GameObject newVeg = Instantiate(veggie, pos, rot);
            float force = Random.Range(_minForce, _maxForce);
            newVeg.GetComponent<Rigidbody>().AddForce(newVeg.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(_minSpawnDelay, _maxSpawnDelay));
        }
    }

}

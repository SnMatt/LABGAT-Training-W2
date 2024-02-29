using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veggie : MonoBehaviour
{
    [SerializeField] private GameObject _whole;
    [SerializeField] private GameObject _sliced;

    [SerializeField] private float _forceMultiplier;

    [SerializeField] private Rigidbody[] _slicedRBs;

    [SerializeField] private int _scoreAmount;

    private Rigidbody _rb;
    private Collider _collider;
    private ParticleSystem _particle;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _particle = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.Direction, blade.transform.position);

            GameManager.Instance.AddScore(_scoreAmount);
        }
    }

    private void Slice(Vector3 direction, Vector3 position)
    {
        _whole.SetActive(false);
        _sliced.SetActive(true);
        _collider.enabled = false;
        direction.y = (direction.y == 0) ? 0.1f : direction.y;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        _sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        foreach (Rigidbody item in _slicedRBs)
        {
            item.velocity = _rb.velocity;
            
            item.AddForceAtPosition(direction * _forceMultiplier, position, ForceMode.Impulse);
        }

        _particle.Play();
    }
}

using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private Animator _animator;

    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].gameObject.layer = LayerMask.NameToLayer("Ragdoll");
        }

        Deactivate();
    }

    public void Activate()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = false;
        }

        _animator.enabled = false;
    }

    public void Deactivate()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = true;
        }

        _animator.enabled = true;
    }
}

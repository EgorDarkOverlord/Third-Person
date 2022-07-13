using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public float Damage;
    private Collider _collider;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _hitSound;



    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }

    private void Start()
    {
        enabled = false;
    }



    public void PlayHitSound()
    {
        _audioSource.PlayOneShot(_hitSound);
    }
}
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private CharacterHealth _health;



    public static void RecieveHealthFromObjectToHitboxes(GameObject gameObject)
    {
        CharacterHealth health = gameObject.GetComponent<CharacterHealth>();

        Hitbox[] hitboxes = gameObject.GetComponentsInChildren<Hitbox>();
        
        for (int i = 0; i < hitboxes.Length; i++)
        {
            hitboxes[i]._health = health;
        }
    }

    public static void AttachHitboxesToRagdoll(Ragdoll ragdoll)
    {
        CharacterJoint[] characterJoints = ragdoll.GetComponentsInChildren<CharacterJoint>();
        
        for (int i = 0; i < characterJoints.Length; i++)
        {
            characterJoints[i].gameObject.AddComponent<Hitbox>();
        }
    }



    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var bullet = collision.gameObject.GetComponent<Bullet>();

        if (bullet != null)
        {          
            _health.Health -= bullet.Damage;
            _rigidbody.AddForceAtPosition(bullet.transform.forward * 200, collision.transform.position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {       
        var hurtbox = other.gameObject.GetComponent<Hurtbox>();

        if (hurtbox != null && hurtbox.transform.root != transform.root)
        {
            hurtbox.PlayHitSound();
            hurtbox.enabled = false;
            _health.Health -= hurtbox.Damage;
            _rigidbody.AddForceAtPosition(hurtbox.transform.forward * 200, other.transform.position, ForceMode.Impulse);
        }
    }
}
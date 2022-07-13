using System.Collections;
using UnityEngine;

public class CharacterFighting : MonoBehaviour
{
    [SerializeField] private HitInfo[] _hits;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _waveSound;

    public bool IsFighting { get; private set; }
    public bool IsHitting { get; private set; }

    private int _animIDFight;
    private int _animIDFightLayer;
    private int _animIDFightIndex;



    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _animIDFight = Animator.StringToHash("Fight");
        _animIDFightIndex = Animator.StringToHash("FightIndex");
        _animIDFightLayer = _animator.GetLayerIndex("Fighting");

        StopFight();
    }



    public void StartFight()
    {
        IsFighting = true;
        _animator.SetBool(_animIDFight, true);
        _animator.SetLayerWeight(_animIDFightLayer, 1);
    }

    public void StopFight()
    {
        IsFighting = false;
        _animator.SetBool(_animIDFight, false);
        _animator.SetInteger(_animIDFightIndex, -1);
        _animator.SetLayerWeight(_animIDFightLayer, 0);
    }

    public HitInfo FindHitByIndex(int fightIndex)
    {
        for (int i = 0; i < _hits.Length; i++)
        {
            if (_hits[i].FightIndex == fightIndex)
            {
                return _hits[i];
            }
        }

        return null;
    }

    public void Hit(int fightIndex)
    {
        StartCoroutine(MakeHit(fightIndex));
    }

    private IEnumerator MakeHit(int fightIndex)
    {
        IsHitting = true;

        _animator.Play("Select", 1);
        yield return new WaitForEndOfFrame();

        _animator.SetInteger(_animIDFightIndex, fightIndex);
        yield return new WaitForEndOfFrame();

        _audioSource.PlayOneShot(_waveSound);

        HitInfo hitInfo = FindHitByIndex(fightIndex);
        yield return new WaitForSeconds(hitInfo.Delay);
        hitInfo.Enable();

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(1).length - hitInfo.Delay);

        _animator.SetInteger(_animIDFightIndex, -1);
        yield return new WaitForEndOfFrame();

        hitInfo.Disable();
        IsHitting = false;
    }
}


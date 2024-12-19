using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    [field : SerializeField] public float Torpeur {  get; set; }
    [field : SerializeField] public float _torpeurObjectif { get; set; }
    [SerializeField] private Movement _movement;
    [SerializeField] private Jump _jump;
    [SerializeField] private Punch _punch;
    [SerializeField] private LaunchCoin _launchCoin;
    [SerializeField] private Role _role;
    [field : SerializeField] public Animator Animator {  get; set; }
    private bool _enabled = false;

    private void Start()
    {
        if (_role.RoleName == "Glouton")
        {
            _torpeurObjectif *= 3;
        }
    }
    void Update()
    {
        if (_movement.IsMoving == false) return;

        if (Torpeur >= _torpeurObjectif)
        {
            StartCoroutine(Wait());

            if (_enabled) return;

            if (!Animator.GetBool("IsStun"))
            {
                Animator.SetBool("IsStun", true);
                StartCoroutine(WaitAnimation());
            }
        }
    }

    IEnumerator Wait()
    {
        Stun(false);
        yield return new WaitForSeconds(6);
        Torpeur = 0;
        Stun(true);
    }

    IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        _enabled = true;
        Animator.SetBool("IsStun", false);
        yield return new WaitForSeconds(2.9f);
        _enabled = false;
    }

    public void Stun(bool booleen)
    {
        _movement.IsMoving = booleen;
        _jump.CanJump = booleen;
        _punch.CanPunch = booleen;
        _launchCoin.CanLaunch = booleen;
    }
}

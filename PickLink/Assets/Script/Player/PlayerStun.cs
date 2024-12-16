using System.Collections;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    [field : SerializeField] public float Torpeur {  get; set; }
    [SerializeField] private float _torpeurObjectif;
    [SerializeField] private Movement _movement;
    [SerializeField] private Jump _jump;
    [SerializeField] private Punch _punch;
    [SerializeField] private LaunchCoin _launchCoin;
    [SerializeField] private Role _role;

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
        }
    }

    IEnumerator Wait()
    {
        Stun(false);
        yield return new WaitForSeconds(10);
        Torpeur = 0;
        Stun(true);

    }

    private void Stun(bool booleen)
    {
        _movement.IsMoving = booleen;
        _jump.CanJump = booleen;
        _punch.CanPunch = booleen;
        _launchCoin.CanLaunch = booleen;
    }
}
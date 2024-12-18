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
        yield return new WaitForSeconds(6);
        Torpeur = 0;
        Stun(true);

    }

    public void Stun(bool booleen)
    {
        _movement.IsMoving = booleen;
        _jump.CanJump = booleen;
        _punch.CanPunch = booleen;
        _launchCoin.CanLaunch = booleen;
    }
}

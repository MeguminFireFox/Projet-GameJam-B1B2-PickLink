using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private GameObject lever;

    public bool IsActive = false;

    [SerializeField]
    Animator animator;

    public void ChangeValueLever()
    {
        if (IsActive)
        {
            animator.SetBool("Activer", false);
        }
        else
        {
            animator.SetBool("Activer", true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (IsActive)
            {
                IsActive = false;
                ChangeValueLever();
            }
            else
            {
                IsActive = true;
                ChangeValueLever();
            }
        }
    }
}

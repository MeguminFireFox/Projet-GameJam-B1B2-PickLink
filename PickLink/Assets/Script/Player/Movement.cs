using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5; // La vitesse du joueur
    [SerializeField] public Vector2 CurrentMovement {  get; set; }
    [SerializeField] public bool IsMoving { get; set; } = true; // Booléen pour empecher le joueur de bouger ou non
    [SerializeField] private Animator _animator;

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!IsMoving) return;

        CurrentMovement = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 mouvement = new Vector3(CurrentMovement.x, 0, CurrentMovement.y);
        mouvement.Normalize();
        transform.Translate(_speed * mouvement * Time.fixedDeltaTime, Space.World);

        if (mouvement != Vector3.zero)
        {
            _animator.SetFloat("Velocity", 7);
            Quaternion toRotation = Quaternion.LookRotation(mouvement, Vector2.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }
        else
        {
            _animator.SetFloat("Velocity", 0);
        }
    }
}

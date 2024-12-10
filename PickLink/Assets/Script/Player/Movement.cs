using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5; // La vitesse du joueur
    private Vector2 _movement;
    [SerializeField] public bool IsMoving { get; set; } = true; // Booléen pour empecher le joueur de bouger ou non

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!IsMoving) return;

        _movement = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 mouvement = new Vector3(_movement.x, 0, _movement.y);
        mouvement.Normalize();
        transform.Translate(_speed * mouvement * Time.fixedDeltaTime, Space.World);

        if (mouvement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(mouvement, Vector2.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }
    }
}

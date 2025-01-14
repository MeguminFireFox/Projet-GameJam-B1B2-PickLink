using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5; // La vitesse du joueur
    [SerializeField] public Vector2 CurrentMovement {  get; set; }
    [SerializeField] public bool IsMoving { get; set; } = true; // Booléen pour empecher le joueur de bouger ou non
    [SerializeField] private Animator _animator;
    private bool _sfx = true;

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

            if (!_sfx) return;

            StartCoroutine(Wait());
        }
        else
        {
            _animator.SetFloat("Velocity", 0);
        }
    }

    IEnumerator Wait()
    {
        GameObject objectSound = SoundObjectPool7.Instance.GetPooledObject();

        if (objectSound != null)
        {
            objectSound.transform.position = transform.position;
            objectSound.SetActive(true);
        }

        _sfx = false;
        yield return new WaitForSeconds(0.5f);
        _sfx = true;
    }
}

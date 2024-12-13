using UnityEngine;

public class PlatformReset : MonoBehaviour
{
    [SerializeField] private bool _isReset = true;

    void Update()
    {
        if (!_isReset) return;

        if (transform.rotation.z >= 0)
        {
            transform.Rotate(0, 0, -0.5f * Time.deltaTime);
        }

        if (transform.rotation.z <= 0)
        {
            transform.Rotate(0, 0, 0.5f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isReset = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isReset = true;
        }
    }
}

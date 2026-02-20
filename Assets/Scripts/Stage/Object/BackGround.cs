using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private Transform _resetPoint;
    [SerializeField] private string _playerLayer = "Player";
    private float _moveSpeed = 2.0f;

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
        {
            transform.position = _resetPoint.position;
        }
    }

    void Move()
    {
        transform.Translate(-transform.forward * _moveSpeed * Time.deltaTime);
    }
}

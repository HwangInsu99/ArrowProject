using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _arrowLayer = "Arrow";
    [SerializeField] private string _playerLayer = "Player";
    [SerializeField] private ItemSpawner _itemSpawner;

    [SerializeField] private float _hp = 80.0f;
    private float _speed = 2.0f;
    private Vector3 _baseScale;
    [SerializeField] private float _scaleUp = 1.1f;
    [SerializeField] private float _recoverSpeed = 10.0f;

    private void Awake()
    {
        _baseScale = transform.localScale;
    }

    void Update()
    {
        EnemyMove();
        transform.localScale = Vector3.Lerp
            (
            transform.localScale,
            _baseScale,
            _recoverSpeed * Time.deltaTime
            );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;
        if (other.gameObject.layer == LayerMask.NameToLayer(_arrowLayer))
        {
            Arrow arrow = other.GetComponent<Arrow>();
            if (arrow == null)
                return;

            // 나중에 시각화 할때 크리티컬 여부에 따라 데미지 색을 바꾸기 위해
            (float damage, bool isCrit )= arrow.Damage();

            EnemyDamaged(damage);
            arrow.OnHit();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
        {
            Player player = other.GetComponent<Player>();
            Debug.Log("충돌시 플레이어 참조");
            if (player == null)
            {
                Debug.Log("Player가 아님");
                return;
            }
            player.PlayerDamaged(_hp);
            EnemyDamaged(_hp);
        }
    }

    void EnemyDamaged(float damage)
    {
        _hp -= damage;
        transform.localScale = _baseScale * _scaleUp; 

        if (_hp <= 0)
        {
            _itemSpawner.SpawnItem();
            Destroy(gameObject);
        }
    }

    void EnemyMove()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void KillZone()
    {
        Destroy(gameObject);
    }
}

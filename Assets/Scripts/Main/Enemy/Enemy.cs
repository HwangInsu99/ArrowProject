using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _attackLayer = "Attack";
    [SerializeField] private string _playerLayer = "Player";

    public float _hp {  get; private set; } = 50.0f;
    private float _speed = 2.0f;
    private Vector3 _baseScale;
    private float _scaleUp = 1.1f;
    private float _recoverSpeed = 10.0f;
    private int _bossScore = 500;
    private int _genScore = 100;
    private int _dropMoney = 2;

    public event Action<float> OnDamaged;
    public event Action<Enemy> OnDead;

    void Start()
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
        if (other.gameObject.layer == LayerMask.NameToLayer(_attackLayer))
        {
            if (other.gameObject.CompareTag("Arrow"))
            {
                Arrow arrow = other.GetComponent<Arrow>();
                if (arrow == null)
                    return;

                // 나중에 시각화 할때 크리티컬 여부에 따라 데미지 색을 바꾸기 위해
                (float damage, bool isCrit) = arrow.Damage();

                EnemyDamaged(damage);
                arrow.OnHit();
            }

            if (other.gameObject.CompareTag("Sword"))
            {
                Sword sword = other.GetComponent<Sword>();
                if (sword == null) return;

                float damage = sword.Damage();
                EnemyDamaged(damage);
                sword.OnHit();
            }
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
            bool isEnd = player.PlayerDamaged(_hp);
            if (!isEnd)
            {
                EnemyDamaged(_hp);
            }            
        }
    }

    void EnemyDamaged(float damage)
    {
        _hp -= damage;
        transform.localScale = _baseScale * _scaleUp;
        OnDamaged?.Invoke(_hp);

        if (_hp > 0)
            return;

        if (gameObject.CompareTag("Boss"))
        {
            GameManager.Instance.IncreaseScore(_bossScore);
            GameManager.Instance.IncreaseMoney(_dropMoney);
            GameManager.Instance.CallUpgradeUI();
            GameManager.Instance.PauseGame(true);
        }
        else
        {
            GameManager.Instance.CallItem(transform.position);
            GameManager.Instance.IncreaseScore(_genScore);
        }            

        Break();
    }

    void EnemyMove()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void Break()
    {
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }
}

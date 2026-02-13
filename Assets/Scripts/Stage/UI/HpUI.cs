using TMPro;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    private Camera _camera;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        if (_target.TryGetComponent<Player>(out Player player))
        {
            SetHpUI(_target.GetComponent<Player>());
        }
    }

    public void SetHpUI(Transform target, Enemy enemy)
    {
        _target = target;

        enemy.OnDamaged += ChangeHp;
        enemy.OnDead += Release;
        ChangeHp(enemy._hp);
    }

    void SetHpUI(Player player)
    {
        Debug.Log("플레이어 연결");
        player.OnHpChanged += ChangeHp;
        player.OnDead += Release;
        ChangeHp(player._hp);
    }

    void Update()
    {
        if (_target == null)
            return;

        Vector3 screenPos = _camera.WorldToScreenPoint(_target.position + _offset);
        transform.position = screenPos;
        float dist = Vector3.Distance(_camera.transform.position, _target.transform.position);
        float dynamicScale = Mathf.Clamp(1.0f / dist * 10f, 0.6f, 1.5f);
        transform.localScale = Vector3.one * dynamicScale;
    }

    void ChangeHp(float hp)
    {
        _hpText.text = Mathf.CeilToInt(hp).ToString();
    }

    void Release(Enemy enemy)
    {
        enemy.OnDamaged -= ChangeHp;
        enemy.OnDead -= Release;
        Destroy(gameObject);
    }

    void Release(Player player)
    {
        player.OnHpChanged -= ChangeHp;
        player.OnDead -= Release;
        Destroy(gameObject);
    }
}

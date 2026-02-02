using TMPro;
using UnityEngine;

public class HpUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    private Camera _camera;

    public void SetHpUI(Transform target, Enemy enemy)
    {
        _target = target;
        _camera = Camera.main;

        enemy.OnDamaged += ChangeHp;
        enemy.OnDead += Release;
        ChangeHp(enemy._hp);
    }

    void Update()
    {
        if (_target == null)
            return;

        Vector3 screenPos = _camera.WorldToScreenPoint(_target.position + _offset);
        transform.position = screenPos;
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
}

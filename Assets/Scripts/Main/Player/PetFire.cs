using UnityEngine;

public class PetFire : MonoBehaviour
{
    [SerializeField] private Pet _parent;
    private float _damage;
    private float _speed = 5.0f;

    private void Awake()
    {
        _parent = GetComponentInParent<Pet>();
    }
    void Start()
    {
        transform.position -= new Vector3(0.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    public void SetDamage(float damage) => _damage = damage;

    public float Damage() => _damage;

    public void Despawn() => _parent.DespawnFire(gameObject);
}

using UnityEngine;

public class Finder : MonoBehaviour
{
    [SerializeField] private SwordManager _swordManager;
    [SerializeField] private LayerMask _targetMask = LayerMask.GetMask();
    [SerializeField] private string _targetLayer = "Enemy";
    [SerializeField] private float _findRadius = 8.0f;
    [SerializeField] private float _findInterval = 0.2f;

    private float _nextFindTime = 0.0f;
    public Transform _currentTarget { get; private set; }
    private bool _hadTarget = false;

    void Awake()
    {
        if (_swordManager == null)
        {
            Debug.Log("sword매니저 참조 받음");
            _swordManager = GetComponentInChildren<SwordManager>();
        }
        _targetMask = LayerMask.GetMask(_targetLayer);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
            _swordManager.TargetLost();
        if (_swordManager == null)
            return;

        if (Time.time < _nextFindTime)
            return;

        _nextFindTime = Time.time + _findInterval;

        FindTarget();
    }

    void FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _findRadius, _targetMask);

        Transform best = null;
        float bestSqr = float.MaxValue;

        for (int i = 0; i < hits.Length; i++)
        {

            Transform t = hits[i].transform;

            Vector3 to = t.position - transform.position;
            float sqr = to.sqrMagnitude;

            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = t;
            }
        }

        if (best != _currentTarget)
        {
            Debug.Log("타겟 변경");
            _currentTarget = best;
            _hadTarget = true;
            _swordManager.SendTarget(_currentTarget);
            return;
        }
        if (_hadTarget && _currentTarget == null)
        {
            _hadTarget = false;
            _swordManager.SendTarget(_currentTarget);
        }
            
    }

    public void IncreasArea()
    {
        _findRadius *= 1.1f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _findRadius);

        if (_currentTarget != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _currentTarget.position);
        }
    }
}

using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private ArrowSpawner _spawner;
    [SerializeField] private string _targetLayer = "Arrow";

    private void Start()
    {
        if (_spawner == null)
            Debug.LogError("이 킬존에 스포너 참조 안했음", this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer(_targetLayer))
        {
            Debug.Log("화살 닿았음");
            _spawner.DespawnArrow(other.gameObject);
        }
    }
}

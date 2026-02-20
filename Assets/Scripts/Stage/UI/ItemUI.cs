using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _explainText;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    private Camera _camera;

    public void SetItemUI(Transform target, Item item)
    {
        
        _target = target;
        _camera = Camera.main;

        _explainText.text = item.Explain();
        item.OnBreaked += Release;
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

    void Release(Item item)
    {
        item.OnBreaked -= Release;
        Destroy(gameObject);
    }
}

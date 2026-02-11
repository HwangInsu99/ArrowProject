using System;
using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _explainText;
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
    }

    void Release(Item item)
    {
        item.OnBreaked -= Release;
        Destroy(gameObject);
    }
}

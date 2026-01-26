using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private Renderer _floorRenderer;
    [SerializeField ]float _floorSpeed = 0.5f;

    Vector2 _offset;
    Material _floorMaterial;

    void Awake()
    {
        if (_floorRenderer == null)
        {
            _floorRenderer = GetComponent<Renderer>();
            if (_floorRenderer == null)
            {
                print("¿ŒΩ∫∆Â≈Õ ∑ª¥ı∑Ø »Æ¿Œ");
                return;
            }
        }
        _floorMaterial = _floorRenderer.material;
    }

    void Update()
    {
        FlowFloor();
    }

    void FlowFloor()
    {
        if (_floorMaterial == null)
            return;

        _offset += Vector2.down * _floorSpeed * Time.deltaTime;

        _floorMaterial.mainTextureOffset = _offset;
    }
}

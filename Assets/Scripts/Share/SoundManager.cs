using UnityEngine;

public enum EBgmType
{
    Main,
    Lobby
}

public enum ESfxType
{
    Arrow,
    Sword,
    EnemyHit
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [Header("BGM")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip[] _bgmClip;

    [Header("SFX")]
    [SerializeField] private AudioSource _arrowSource;
    [SerializeField] private AudioSource _swordSource;
    [SerializeField] private AudioSource _hitSource;
    [SerializeField] private AudioClip[] _sfxClip;

    [Header("옵션")]
    [SerializeField, Range(0f, 1f)] private float _masterVolume = 1.0f;
    [SerializeField, Range(0f, 1f)] private float _bgmVolume = 1.0f;
    [SerializeField, Range(0f, 1f)] private float _sfxVolume = 1.0f;
    [SerializeField] private bool _randomPitch = true;
    [SerializeField] private Vector2 _pitchRange = new Vector2(0.95f, 1.05f);
    private float _hitVolume = 0.5f;
    private float _swordVolume = 0.45f;
    private float _arrowSoundCool = 0.2f;
    private float _arrowSoundTimer;

    public float MasterVolume => _masterVolume;
    public float BGMVolume => _bgmVolume;
    public float SfxVolume => _sfxVolume;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = false;
        _bgmSource.priority = 0;

        _arrowSource = gameObject.AddComponent<AudioSource>();
        _arrowSource.playOnAwake = false;
        _swordSource = gameObject.AddComponent<AudioSource>();
        _swordSource.playOnAwake = false;
        _hitSource = gameObject.AddComponent<AudioSource>();
        _hitSource.playOnAwake = false;
    }

    private void Start()
    {
        PlayBGM(EBgmType.Main);
    }

    private void Update()
    {
        if (_arrowSoundTimer > 0)
            _arrowSoundTimer -= Time.deltaTime;
    }

    private void PlayBGM(EBgmType type)
    {
        if(_bgmClip == null)
        {
            Debug.LogWarning("bgmClip 비어있음 / 인스펙터 확인");
            return;
        }
        _bgmSource.volume = _bgmVolume * _masterVolume * 0.5f;
        _bgmSource.clip = _bgmClip[(int)type];
        _bgmSource.Play();
    }

    public void PlaySfx(ESfxType type)
    {
        if (_sfxClip == null)
        {
            Debug.LogWarning("sfxClip 비어있음 / 인스펙터 확인");
            return;
        }

        switch (type)
        {
            case ESfxType.Arrow:
                if (_arrowSoundTimer > 0)
                    return;
                _arrowSoundTimer = _arrowSoundCool;
                ApplyPitch(_arrowSource);
                _arrowSource.volume = _sfxVolume * _masterVolume;
                _arrowSource.PlayOneShot(_sfxClip[(int)type]);
                break;
            case ESfxType.Sword:
                ApplyPitch(_swordSource);
                _swordSource.volume = _swordVolume * _sfxVolume * _masterVolume;
                _swordSource.Stop();
                _swordSource.PlayOneShot(_sfxClip[(int)type]);
                break;
            case ESfxType.EnemyHit:
                ApplyPitch(_hitSource);
                _hitSource.volume = _hitVolume * _sfxVolume * _masterVolume;
                _hitSource.Stop();
                _hitSource.PlayOneShot(_sfxClip[(int)type]);
                break;
        }
    }

    void ApplyPitch(AudioSource source)
    {
        source.pitch = _randomPitch ? Random.Range(_pitchRange.x, _pitchRange.y) : 1.0f;
    }

    public void MasterVolumeChange(float value)
    {
        _masterVolume = value;
        _bgmSource.volume = _bgmVolume * _masterVolume * 0.5f;
    }

    public void BGMVolumeChange(float value)
    {
        _bgmVolume = value;
        _bgmSource.volume = _bgmVolume * _masterVolume * 0.5f;
    }

    public void SfxVolumeChange(float value)
    {
        _sfxVolume = value;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}

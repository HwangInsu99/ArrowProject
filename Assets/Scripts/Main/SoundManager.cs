using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BgmType
{
    Main,
    Lobby
}

public enum SfxType
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
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip[] _sfxClip;

    [Header("옵션")]
    [SerializeField, Range(0f, 1f)] private float _masterVolume = 1.0f;
    [SerializeField, Range(0f, 1f)] private float _bgmVolume = 1.0f;
    [SerializeField, Range(0f, 1f)] private float _sfxVolume = 1.0f;
    [SerializeField] private bool _randomPitch = true;
    [SerializeField] private Vector2 _pitchRange = new Vector2(0.95f, 1.05f);

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = false;

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.playOnAwake = false;
    }

    private void Start()
    {
        PlayBGM(BgmType.Main);
    }

    private void PlayBGM(BgmType type)
    {
        if(_bgmClip == null)
        {
            Debug.LogWarning("bgmClip 비어있음 / 인스펙터 확인");
            return;
        }
        _bgmSource.volume = _bgmVolume * _masterVolume;
        _bgmSource.clip = _bgmClip[(int)type];
        _bgmSource.Play();
    }

    public void PlaySfx(SfxType type)
    {
        // 효과음  0 = 화살, 1 = 검, 2 = 적 피격
        if (_sfxClip == null)
        {
            Debug.LogWarning("sfxClip 비어있음 / 인스펙터 확인");
            return;
        }

        if (_randomPitch)
        {
            _sfxSource.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        }

        else
        {
            _sfxSource.pitch = 1.0f;
        }

        _sfxSource.PlayOneShot(_sfxClip[(int)type], _sfxVolume * _masterVolume);
    }
}

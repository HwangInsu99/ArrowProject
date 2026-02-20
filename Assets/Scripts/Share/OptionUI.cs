using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Slider _master;
    [SerializeField] private Slider _bgm;
    [SerializeField] private Slider _sfx;

    private void OnEnable()
    {
        SyncSlider();
    }

    void SyncSlider()
    {
        _master.value = SoundManager.Instance.MasterVolume;
        _bgm.value = SoundManager.Instance.BGMVolume;
        _sfx.value = SoundManager.Instance.SfxVolume;
    }

    public void SliderMasterChange(float value)
    {
        SoundManager.Instance.MasterVolumeChange(value);
    }

    public void SliderBGMChanged(float value)
    {
        SoundManager.Instance.BGMVolumeChange(value);
    }
    public void SliderSfxChanged(float value)
    {
        SoundManager.Instance.SfxVolumeChange(value);
    }
}

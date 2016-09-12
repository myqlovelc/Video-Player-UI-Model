using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaybackControlManager : ViewBase {

    public Slider PositionSlider;

    public Slider SoundSlider;

    public VideoSettingManager VideoSettingManager;

    public PlayListItem ViewModel
    {
        get { return DataContext as PlayListItem; }
    }

    private bool IsVideoSettingPanelOn = false;

	// Use this for initialization
	void Start () {
        PositionSlider.onValueChanged.AddListener(OnPositionSliderValueChanged);
        SoundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPositionSliderValueChanged(float value)
    {
        Debug.Log("Position : " + value);
    }

    public void OnReplayButtonClicked()
    {

    }

    public void OnPauseAndResumeButtonClicked()
    {

    }

    public void OnSoundSliderValueChanged(float value)
    {
        Debug.Log("Sound : " + value);
    }

    public void OnVideoSettingButtonClicked()
    {
        if (!IsVideoSettingPanelOn)
        {
            VideoSettingManager.SetDataContext(ViewModel);
            ScreenManager._Instance.ShowVideoSettingPanel();
            IsVideoSettingPanelOn = true;
        }
        else
        {
            ScreenManager._Instance.CloseVideoSettingPanel();
            IsVideoSettingPanelOn = false;
        }
    }

    public void OnBackToMenuButtonClicked()
    {
        ScreenManager._Instance.ShowPlayListPanel();
    }
}

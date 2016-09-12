using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour {

    public enum SCREEN_TYPE
    {
        SCREEN_PLAY_LIST,
        SCREEN_FILE_BROWSER,
        SCREEN_VIDEO_SETTING,
        SCREEN_PLAYBACK_CONTROL
    }

    public RectTransform PlayListDisplayPanel;

    public RectTransform PlayListManagePanel;

    public RectTransform VideoSettingPanel;

    public RectTransform PlaybackControlPanel;

    public static ScreenManager _Instance
    {
        get;
        private set;
    }

    void Awake ()
    {
        if (_Instance != null)
        {
            Debug.LogWarning(this + ": Multi-Instance not allowed");
            Destroy(this);
            return;
        }

        _Instance = this;
    }

	// Use this for initialization
    void Start()
    {
        PlayListDisplayPanel.gameObject.SetActive(true);
        PlayListManagePanel.gameObject.SetActive(false);
        VideoSettingPanel.gameObject.SetActive(false);
        PlaybackControlPanel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowFileBrowserPanel()
    {
        PlayListDisplayPanel.gameObject.SetActive(true);
        PlayListManagePanel.gameObject.SetActive(true);
        VideoSettingPanel.gameObject.SetActive(false);
        PlaybackControlPanel.gameObject.SetActive(false);

        PlayListDisplayPanel.anchoredPosition = new Vector2(-350, 0);
        PlayListManagePanel.anchoredPosition = new Vector2(350, 0);
    }

    public void CloseFileBrowserPanel()
    {
        PlayListDisplayPanel.gameObject.SetActive(true);
        PlayListManagePanel.gameObject.SetActive(false);
        PlaybackControlPanel.gameObject.SetActive(false);

        PlayListDisplayPanel.anchoredPosition = new Vector2(0, 0);
    }

    public void ShowVideoSettingPanel()
    {
        VideoSettingPanel.gameObject.SetActive(true);

        if (PlayListDisplayPanel.gameObject.activeInHierarchy)
        {
            VideoSettingPanel.anchoredPosition = PlayListDisplayPanel.anchoredPosition;
        }
        else if (PlaybackControlPanel.gameObject.activeInHierarchy)
        {
            VideoSettingPanel.anchoredPosition = new Vector2(0, 50);
        }
    }

    public void CloseVideoSettingPanel()
    {
        VideoSettingPanel.gameObject.SetActive(false);
    }

    public void ShowPlaybackControlPanel()
    {
        PlayListDisplayPanel.gameObject.SetActive(false);
        PlayListManagePanel.gameObject.SetActive(false);
        VideoSettingPanel.gameObject.SetActive(false);
        PlaybackControlPanel.gameObject.SetActive(true);

        PlaybackControlPanel.anchoredPosition = new Vector2(0, 0);
    }

    public void ShowPlayListPanel()
    {
        PlayListDisplayPanel.gameObject.SetActive(true);
        PlayListManagePanel.gameObject.SetActive(false);
        VideoSettingPanel.gameObject.SetActive(false);
        PlaybackControlPanel.gameObject.SetActive(false);

        PlayListDisplayPanel.anchoredPosition = new Vector2(0, 0);
    }
}

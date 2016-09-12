using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwitchManager : MonoBehaviour {

    public Button DisplayButton;

    public Button ManageButton;

	// Use this for initialization
	void Start () {
        RegisterUIEvent();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RegisterUIEvent() {
        DisplayButton.onClick.AddListener(OnDisplayButtonClicked);
        ManageButton.onClick.AddListener(OnManageButtonClicked);
    }

    public void OnDisplayButtonClicked()
    {
        ScreenManager._Instance.CloseFileBrowserPanel();
    }

    public void OnManageButtonClicked()
    {
        ScreenManager._Instance.ShowFileBrowserPanel();
    }
}

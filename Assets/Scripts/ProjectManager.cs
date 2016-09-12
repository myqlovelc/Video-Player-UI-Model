using UnityEngine;
using System.Collections;

public class ProjectManager : MonoBehaviour {

    public static ProjectManager _Instance {
        get;
        private set;
    }

    public Texture2D DefaultVideoThumbnail;

    public static string projectSettingFile = "Config/ProjectSetting.xml";

    private ProjectSetting _projectSetting;
    public ProjectSetting ProjectSetting
    {
        get { return _projectSetting; }
        set { _projectSetting = value; }
    }

    void Awake() {
        if (_Instance != null) {
            Debug.LogWarning(this + ": Multi-Instance not allowed");
            Destroy(this);
            return;
        }

        _Instance = this;
    }

	// Use this for initialization
    void Start()
    {
	    _projectSetting = XMLUtil.LoadSetting<ProjectSetting>(projectSettingFile);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class ProjectSetting
{
    public string ImagePath;

    public string FfmpegFile;
}

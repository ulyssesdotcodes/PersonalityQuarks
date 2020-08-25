using UnityEngine;
public class StartEpisodeRecord : StartEpisodeAction
{
    static int Frame = 0;
    static int count = 0;
    private PersonalityQuarksSettings settings;
    private Recorder recorder;
    public void Start()
    {
    }

    public override void OnStartEpisodePreReset()
    {
        if (settings == null)
        {
            settings = GameObject.FindGameObjectWithTag("settings").GetComponent<PersonalityQuarksSettings>();
        }

        if (recorder == null)
        {
            recorder = GetComponent<BaseAgent>().area.GetComponent<Recorder>();
        }

        if (settings.RecordReplay)
        {
            recorder.StopRecording();
            recorder.StartRecording();
        }
    }
}
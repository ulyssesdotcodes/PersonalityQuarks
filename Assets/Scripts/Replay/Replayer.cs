using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
public class Replayer : QuarkEventListener
{
    private int Count = 0;
    private bool IsRecording = false;
    private Queue<QuarkEvent> events;
    // public override int Id
    // {
    //     get
    //     {
    //         return -1;
    //     }
    // }

    public override void OnEvent(QuarkEvent e)
    {
        events.Enqueue(e);
    }
}
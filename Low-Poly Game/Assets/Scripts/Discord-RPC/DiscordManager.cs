using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DiscordPresence;

public class DiscordManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int cur_time = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        PresenceManager.UpdatePresence(detail: "Dans le menu principal", start: cur_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

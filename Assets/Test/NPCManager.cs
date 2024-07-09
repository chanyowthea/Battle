using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager
{
    public static NPCManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new NPCManager();
            }
            return _Instance;
        }
    }

    public static NPCManager _Instance;

    List<NPC> mNpcs = new List<NPC>();
    public void AddNPC(NPC npc)
    { 
        mNpcs.Add(npc);
    }

    public void RemoveNPC(NPC npc)
    {
        mNpcs.Remove(npc);
    }

    public List<NPC> GetNpcs()
    {
        return mNpcs;
    }
}

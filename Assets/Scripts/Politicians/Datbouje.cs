using UnityEngine;
using System.Collections;

public class Datbouje : BasePolitician
{
    void start()
    {
        Setup();
    }

    public override void SetStartStats()
    {
        base.SetStartStats();
        reputation = player.playerrep;

        if (reputation > 65)
        {
            reputation = 65;
        }

        sanity = player.playersan - player.playerrep;

        if (sanity >= 65)
        {
            sanity = 65;
        }

        if (sanity < 35)
        {
            sanity = 35;
        }
    }
}

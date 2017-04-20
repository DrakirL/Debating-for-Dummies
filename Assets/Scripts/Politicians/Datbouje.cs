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
        reputation = player.playerrep + 7;

        if (reputation > 65)
        {
            reputation = 75;
        }

        sanity = player.playersan - 7;

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

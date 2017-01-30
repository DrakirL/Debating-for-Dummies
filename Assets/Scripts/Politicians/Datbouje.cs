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
        sanity = player.playersan;

        if (reputation > 75)
        {
            reputation = 75;
        }

        if (sanity >= 65)
        {
            sanity -= player.playerrep;
        }

        if (sanity < 35)
        {
            sanity = 35;
        }
    }
}

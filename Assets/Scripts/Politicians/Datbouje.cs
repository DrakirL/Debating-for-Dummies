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
        sanity = player.playersan - (player.playerrep / 5);

        if (sanity >= 65)
        {
            sanity = 65;
        }

        base.SetStartStats();
        reputation = (player.playerrep / 2) + (sanity / 2);

        if (reputation > 65)
        {
            reputation = 75;
        }
    }
}

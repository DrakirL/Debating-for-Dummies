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

        if (sanity > 60)
        {
            sanity = 45;
        }

        base.SetStartStats();
        reputation = (player.playerrep / 2) + (sanity / 2);

        if (reputation > 75)
        {
            reputation = 80;
        }
    }
}

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

        sanity = player.playersan * 2 / 3;

        reputation = player.playerrep * 5 / 6;
    }
}

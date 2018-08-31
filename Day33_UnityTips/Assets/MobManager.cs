using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : Singleton<MobManager>
{
    protected MobManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public string myGlobalVar = "whatever";
    public int MobCount() { return 100; }
}
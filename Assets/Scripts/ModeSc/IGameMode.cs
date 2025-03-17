using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameMode
{
    void InitializeMode();
    void UpdateMode();
    void ExitMode();
}


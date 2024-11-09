using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CameraMgr : Singleton<CameraMgr> {
    [Header("Obj Refs")]
    public Camera _mainCamera;

    public override void Awake() {
        base.Awake();
    }
}
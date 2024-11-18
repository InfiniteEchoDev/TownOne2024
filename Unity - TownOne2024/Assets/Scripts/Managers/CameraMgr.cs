using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;


public class CameraMgr : Singleton<CameraMgr> {
    [FormerlySerializedAs("_mainCamera")] [Header("Obj Refs")]
    public Camera MainCamera;

    public override void Awake() {
        base.Awake();
    }
}
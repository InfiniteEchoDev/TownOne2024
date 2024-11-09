using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Singleton<TSingletonClass> : MonoBehaviour where TSingletonClass : MonoBehaviour {

    public static TSingletonClass Instance { get; private set; }

    public virtual void Awake() {
        if( Instance != null ) {
            Destroy( gameObject );
            return;
        }

        Instance = this as TSingletonClass;
    }

    void OnApplicationQuit() {
        Destroy( gameObject );
        Instance = null;
    }
}
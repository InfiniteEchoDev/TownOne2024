using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Singleton<SingletonClass> : MonoBehaviour where SingletonClass : MonoBehaviour {

    public static SingletonClass Instance { get; private set; }

    public virtual void Awake() {
        if( Instance != null ) {
            Destroy( gameObject );
            return;
        }

        Instance = this as SingletonClass;
    }

    void OnApplicationQuit() {
        Destroy( gameObject );
        Instance = null;
    }
}
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CoroutineUtilities : MonoBehaviour {
    static public IEnumerator WaitAFrameAndExecute( System.Action execute ) {
        yield return null;

        execute();
    }
}
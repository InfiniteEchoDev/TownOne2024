using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CoroutineUtilities : MonoBehaviour {
    static public IEnumerator WaitAFrameAndExecute( System.Action execute ) {
        yield return null;

        execute();
    }

    static public IEnumerator WaitForFixedUpdateFrameAndExecute( System.Action execute ) {
        yield return new WaitForFixedUpdate();

        execute();
    }
}
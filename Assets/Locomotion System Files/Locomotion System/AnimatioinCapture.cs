using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// using UnityEngine.Animation;

public class AnimatioinCapture : MonoBehaviour
{
    AnimationClip RecordClip;
    // Start is called before the first frame update
    void Start()
    {
        RecordClip = new AnimationClip();
        RecordClip.legacy = true;
    }

    void RecordTransformKey(Transform tr, string parent) {
        string key = tr.name;
        if (!string.IsNullOrEmpty(parent)) {
            key = parent + "/" + key; 
        }
        AnimationCurve curve; 
        curve = AnimationCurve.Constant(0, 1, tr.localPosition.x);
        RecordClip.SetCurve(key, typeof(Transform), "localPosition.x", curve);
        curve = AnimationCurve.Constant(0, 1, tr.localPosition.y);
        RecordClip.SetCurve(key, typeof(Transform), "localPosition.y", curve);
        curve = AnimationCurve.Constant(0, 1, tr.localPosition.z);
        RecordClip.SetCurve(key, typeof(Transform), "localPosition.z", curve);

        curve = AnimationCurve.Constant(0, 1, tr.localRotation.x);
        RecordClip.SetCurve(key, typeof(Transform), "localRotation.x", curve);
        curve = AnimationCurve.Constant(0, 1, tr.localRotation.y);
        RecordClip.SetCurve(key, typeof(Transform), "localRotation.y", curve);
        curve = AnimationCurve.Constant(0, 1, tr.localRotation.z);
        RecordClip.SetCurve(key, typeof(Transform), "localRotation.z", curve);

        foreach(Transform child in tr) {
            RecordTransformKey(child, key);
        }
    }
    // Update is called once per frame
    void Update()
    {
		// var controlMotionState = GetComponent<Animation>()["LocomotionSystem"];
        // if (controlMotionState == null)
        // {
        //     return;
        // }
        // Create dummy animation state with control motion name
        // AnimationClip clip = new AnimationClip();
        RecordClip.ClearCurves();
        foreach(Transform child in transform) {
            RecordTransformKey(child, "");
        }
        
        if ( Input.GetKeyDown(KeyCode.O) ) {
            // save clip
            AssetDatabase.CreateAsset(RecordClip, "Assets/MyAnim.anim");
            AssetDatabase.SaveAssets();
        }
    }
}

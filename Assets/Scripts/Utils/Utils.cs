using UnityEngine;
using System.Collections;
using System;

public class Utils
{
    public static void Normalize(Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localEulerAngles = Vector3.zero;
        t.localScale = Vector3.one;
    }

    public static GameObject InstantiateTo(GameObject parent, GameObject go)
    {
        GameObject ins = (GameObject)GameObject.Instantiate(
            go,
            parent.transform.position,
            parent.transform.rotation
        );
        ins.transform.parent = parent.transform;
        Normalize(ins.transform);
        return ins;
    }

    public static T InstantiateTo<T>(GameObject parent, GameObject go)
        where T : Component
    {
        return InstantiateTo(parent, go).GetComponent<T>();
    }
}
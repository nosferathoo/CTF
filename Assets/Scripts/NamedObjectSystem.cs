using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedObjectsSystem : Singleton<NamedObjectsSystem>
{
    private Dictionary<string, GameObject> _namedObjects = new Dictionary<string, GameObject>();

    public GameObject GetObjectByName(string objName)
    {
        return !_namedObjects.ContainsKey(objName) ? null : _namedObjects[objName];
    }
    
    public void RegisterObject(string objName, GameObject obj)
    {
        if (_namedObjects.ContainsKey(objName))
        {
            Debug.LogError($"Named object {objName} already registered!");
            return;
        }
        
        _namedObjects.Add(objName, obj);
    }

    public void UnregisterObject(string objName)
    {
        if (!_namedObjects.ContainsKey(objName))
        {
            Debug.LogError($"No gameobject with {objName} name");
            return;
        }

        _namedObjects.Remove(objName);
    }
}

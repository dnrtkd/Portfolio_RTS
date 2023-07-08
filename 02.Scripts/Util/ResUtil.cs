using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResUtil
{
    public static T Load<T>(string _path) where T : Object
    {
        if (string.IsNullOrWhiteSpace(_path) == true)
        {
            Debug.LogError("ResUtill::Load() null==path" + _path);
            return null;
        }

        T _res = Resources.Load<T>(_path);
        if (null == _res)
        {
            Debug.LogError("ResUtill::Load() No have File : " + _path);
            return null;
        }
        return _res;
    }

    public static GameObject Create(string _path, Transform _parent=null)
    {
        GameObject _res = Load<GameObject>(_path);
        if (null == _res)
            return null;

        GameObject _instantiate = GameObject.Instantiate<GameObject>(_res);
        if (null != _parent)
        {
            _instantiate.transform.SetParent(_parent);
        }

        return _instantiate;
    }

    public static T Create<T>(string _path, Transform _Parent=null) where T : Component
    {
        T _res = Load<T>(_path);
        T _ins = GameObject.Instantiate<T>(_res,_Parent);
        if (null == _ins)
        {
            Debug.LogError("ResUtill::Create() no component : " + _path);
            return null;
        }

        if (null != _Parent)
        {
            _ins.transform.SetParent(_Parent);
        }
        return _ins;
    }

    public static string EnumToString<T>(T enumValue) where T : System.Enum
    {
        return System.Enum.GetName(typeof(T), enumValue);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
            return raycastHit.point;
        else
            return Vector3.zero;
    }
}

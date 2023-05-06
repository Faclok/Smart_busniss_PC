using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Assets.MultiSetting;

/// <summary>
/// ����� ������
/// </summary>
public static class ExceptionCatcher
{

    /// <summary>
    /// ����� ��� ������ ������ � ����������� ����������� ������, � �������� ����� ����� �����
    /// </summary>
    public static event Action<string> ExceptionConverter;

    /// <summary>
    /// �������� ������ ������
    /// </summary>
    /// <param name="error"></param>
    [Obsolete]
    public static void PutException(string error)
    {
        Debug.Log(error);
        ExceptionConverter?.Invoke(error);
    }

    /// <summary>
    /// ����� ������
    /// </summary>
    /// <param name="result"></param>
    public static void Debugger(this Result result) 
    {
        var exception = result.TypeException + ": " + result.Exception;
        ExceptionConverter?.Invoke(exception);
        Debug.Log(exception);
    }

    /// <summary>
    /// ����� ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    public static void Debugger<T>(this ResultOf<T> result)
    {
        var exception = result.TypeException + ": " + result.Exception;
        ExceptionConverter?.Invoke(exception);
        Debug.Log(exception);
    }
}


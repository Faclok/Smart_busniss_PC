using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Assets.MultiSetting;

/// <summary>
/// Отлов ошибок
/// </summary>
public static class ExceptionCatcher
{

    /// <summary>
    /// Класс для отлова ошибок и дальнейшего исправления ошибок, с мпомощью логов этого файла
    /// </summary>
    public static event Action<string> ExceptionConverter;

    /// <summary>
    /// Добавить ошибку ошибку
    /// </summary>
    /// <param name="error"></param>
    [Obsolete]
    public static void PutException(string error)
    {
        Debug.Log(error);
        ExceptionConverter?.Invoke(error);
    }

    /// <summary>
    /// Отлов ошибки
    /// </summary>
    /// <param name="result"></param>
    public static void Debugger(this Result result) 
    {
        var exception = result.TypeException + ": " + result.Exception;
        ExceptionConverter?.Invoke(exception);
        Debug.Log(exception);
    }

    /// <summary>
    /// Отлов ошибки
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


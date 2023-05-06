using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.View.Body.Debugger
{

    public static class Debugger
    {
        private readonly static StringBuilder _debugLog = new();

        static Debugger()
        {
            ExceptionCatcher.ExceptionConverter += AddDebug;
        }

        public static void AddDebug(string log)
        {
            _debugLog.AppendLine(log);
        }
    }
}

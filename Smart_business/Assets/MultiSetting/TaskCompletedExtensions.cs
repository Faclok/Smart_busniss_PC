using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.MultiSetting
{
    public static class TaskCompletedExtensions
    {
        private static readonly List<IDisposable> _tasksDispose = new();

        public static TaskCompleted<TResult> GetTaskCompleted<TResult>(this Task<TResult> task, Action<TResult> action)
        {
            var result = new TaskCompleted<TResult>(task, action);
            _tasksDispose.Add(result);

            return result;
        }

        public static void ClearTaskCompleted<TTask,TResult>(TTask task)
            where TTask : TaskCompleted<TResult>
        {
            if(_tasksDispose.Contains(task))
                _tasksDispose.Remove(task);
        }

        public static TaskCompleted GetTaskCompleted(this Task task, Action action)
        {
            var result = new TaskCompleted(task, action);
            _tasksDispose.Add(result);

            return result;
        }

        public static void ClearTaskCompleted(TaskCompleted task)
        {
            if (_tasksDispose.Contains(task))
                _tasksDispose.Remove(task);
        }

        public static void ClearAllTasks()
        {
            foreach (var item in _tasksDispose)
                item?.Dispose();

            _tasksDispose.Clear();
        }
    }
}

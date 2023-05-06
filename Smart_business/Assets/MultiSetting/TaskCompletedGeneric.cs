using System;
using System.Threading.Tasks;
using Assets.ViewModel;

#nullable enable annotations

namespace Assets.MultiSetting
{
    public class TaskCompleted<TResult> : IDisposable
    {
        public readonly Task<TResult> Task;

        private readonly Action<TResult> _onCompleted;

        public TResult Result => Task.Result;

        public TaskCompleted(Task<TResult> task, Action<TResult> onCompleted)
        {
            Task = task;

            if (Task == null)
                new Result(exception: "Task is null load", TypeException.LogicApplication);

            _onCompleted = onCompleted;
            TimerDispatcher.Tick += Tick;
        }

        public bool IsCompleted => Task.IsCompleted;

        private void Tick()
        {
            if (Task.IsCompleted)
            {
                _onCompleted(Task.Result);
                TimerDispatcher.Tick -= Tick;
            }
        }

        public void Dispose()
        {
            Task?.Dispose();
            TimerDispatcher.Tick -= Tick;

            TaskCompletedExtensions.ClearTaskCompleted<TaskCompleted<TResult>,TResult>(this);
        }

        public static implicit operator bool(TaskCompleted<TResult> task) => task?.IsCompleted ?? true;
    }

    public class TaskCompleted : IDisposable
    {
        public readonly Task Task;

        private readonly Action _onCompleted;

        public TaskCompleted(Task task, Action onCompleted)
        {
            Task = task;

            if (Task == null)
                new Result(exception: "Task is null load", TypeException.LogicApplication);

            _onCompleted = onCompleted;
            TimerDispatcher.Tick += Tick;
        }

        public bool IsCompleted => Task.IsCompleted;

        private void Tick()
        {
            if (Task.IsCompleted)
            {
                _onCompleted();
                TimerDispatcher.Tick -= Tick;
            }
        }

        public void Dispose()
        {
            Task?.Dispose();
            TimerDispatcher.Tick -= Tick;

            TaskCompletedExtensions.ClearTaskCompleted(this);
        }

        public static implicit operator bool(TaskCompleted task) => task?.IsCompleted ?? true;
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nito.AsyncEx.Internal.PlatformEnlightenment
{
    public sealed class SingleThreadedApartmentThread
    {
        private readonly object _thread;

        public SingleThreadedApartmentThread(Action execute, bool sta)
        {
            if (sta) throw new NotSupportedException();
            _thread = sta ? new ThreadTask(execute) : (object)Task.Factory.StartNew(execute, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public Task JoinAsync()
        {
            var ret = _thread as Task;
            if (ret != null)
                return ret;
            return ((ThreadTask)_thread).Task;
        }

        private sealed class ThreadTask
        {
            private readonly TaskCompletionSource<object> _tcs;
            private readonly Thread _thread;
            public ThreadTask(Action execute)
            {
#if NET451
                _tcs = new TaskCompletionSource<object>();
                _thread = new Thread(() =>
                {
                    try
                    {
                        execute();
                    }
                    finally
                    {
                        _tcs.TrySetResult(null);
                    }
                });
                _thread.SetApartmentState(ApartmentState.STA);
                _thread.Name = "STA AsyncContextThread (Nito.AsyncEx)";
                _thread.Start();
#endif
            }
            public Task Task
            {
                get { return _tcs.Task; }
            }
        }
    }
}

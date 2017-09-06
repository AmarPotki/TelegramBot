using System;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramBot.Common{
    public delegate void TimerCallback(object state);

    public sealed class Timer : CancellationTokenSource, IDisposable
    {
        private readonly TimerCallback _callback;
        private readonly object _state;
        private readonly int _dueTime;
        private int _period;

        public Timer(TimerCallback callback, object state, int dueTime, int period){
            _callback = callback;
            _state = state;
            _dueTime = dueTime;
            _period = period;
        }

        private void CreateTimer(TimerCallback callback, object state, int dueTime, int period){
            _period = period;
            Task.Delay(dueTime, Token).ContinueWith(async (t, s) =>{
                var tuple = (Tuple<TimerCallback, object>) s;

                while (true){
                    if (IsCancellationRequested){
                        break;
                    }
                    await Task.Run(() => tuple.Item1(tuple.Item2));
                    await Task.Delay(period);
                }
            }, Tuple.Create(callback, state), CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
                TaskScheduler.Default);
        }

        public void Stop(){
            if (!IsCancellationRequested){
                Cancel();
                Dispose();
            }
        }

        public void Start(){
            CreateTimer(_callback, _state, _dueTime, _period);
        }
    }
}
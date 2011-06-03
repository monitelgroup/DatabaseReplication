using System;
using System.Threading;

namespace replication
{
	public class EventTimer
	{
		Timer _tmr;
		AutoResetEvent _autoEvent;
		TimerCallback _tcb;
		int _interval;
		
		public EventTimer(int timerInterval, TimerCallback callBack){
			this._interval = timerInterval;
			this._autoEvent = new AutoResetEvent(false);
			this._tcb = callBack;
		}
		
		public void Start() {
			this._tmr = new Timer(this._tcb, this._autoEvent, 0, this._interval);
			this._autoEvent.WaitOne(System.Threading.Timeout.Infinite, false);
		}
		
		public void ChangeInterval(int newInterval) {
			this._interval = newInterval;
			this._tmr.Change(0, this._interval);
		}
		
		public void Stop() {
			this._autoEvent.Close();
			this._tmr.Dispose();
		}
	}
}


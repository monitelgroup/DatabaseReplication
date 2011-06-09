using System;
using System.Threading;

namespace replication
{
    /// <summary>
    /// ����� ���������� �� ������ ��������
    /// </summary>
	public class EventTimer
	{

		Timer _tmr;
		AutoResetEvent _autoEvent;
		
        /// <summary>
        /// CallBack ������� ������� ����� ������� �� ������� �������
        /// </summary>
        TimerCallback _tcb;
		
        /// <summary>
		/// �������� ����� ������� ����� ����������� ������
		/// </summary>
        int _interval;
		
        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="timerInterval">
        /// �������� ����� ������� ����� ����������� ������
        /// </param>
        /// <param name="callBack">
        /// CallBack ������� ������� ����� ������� �� ������� �������
        /// </param>
		public EventTimer(int timerInterval, TimerCallback callBack){
			this._interval = timerInterval;
			this._autoEvent = new AutoResetEvent(false);
			this._tcb = callBack;
		}
		
        /// <summary>
        /// ������ �������
        /// </summary>
		public void Start() {
			this._tmr = new Timer(this._tcb, this._autoEvent, 0, this._interval);
			this._autoEvent.WaitOne(System.Threading.Timeout.Infinite, false);
		}
		
        /// <summary>
        /// ����� ��������� ������������ �������
        /// </summary>
        /// <param name="newInterval">
        /// ����� �������� ���������
        /// </param>
		public void ChangeInterval(int newInterval) {
			this._interval = newInterval;
			this._tmr.Change(0, this._interval);
		}
		
        /// <summary>
        /// ��������� �������
        /// </summary>
		public void Stop() {
			this._autoEvent.Close();
			this._tmr.Dispose();
		}
	}
}


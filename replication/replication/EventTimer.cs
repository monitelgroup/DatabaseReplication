using System;
using System.Threading;

namespace replication
{
    /// <summary>
    /// Класс отвечающий за работу таймеров
    /// </summary>
	public class EventTimer
	{

		Timer _tmr;
		AutoResetEvent _autoEvent;
		
        /// <summary>
        /// CallBack функция которая будет вызвана по событию таймера
        /// </summary>
        TimerCallback _tcb;
		
        /// <summary>
		/// Интервал через который будет срабатывать таймер
		/// </summary>
        int _interval;
		
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="timerInterval">
        /// Интервал через который будет срабатывать таймер
        /// </param>
        /// <param name="callBack">
        /// CallBack функция которая будет вызвана по событию таймера
        /// </param>
		public EventTimer(int timerInterval, TimerCallback callBack){
			this._interval = timerInterval;
			this._autoEvent = new AutoResetEvent(false);
			this._tcb = callBack;
		}
		
        /// <summary>
        /// Запуск таймера
        /// </summary>
		public void Start() {
			this._tmr = new Timer(this._tcb, this._autoEvent, 0, this._interval);
			this._autoEvent.WaitOne(System.Threading.Timeout.Infinite, false);
		}
		
        /// <summary>
        /// Смена интервала срабатывания таймера
        /// </summary>
        /// <param name="newInterval">
        /// Новое значение интервала
        /// </param>
		public void ChangeInterval(int newInterval) {
			this._interval = newInterval;
			this._tmr.Change(0, this._interval);
		}
		
        /// <summary>
        /// Остановка таймера
        /// </summary>
		public void Stop() {
			this._autoEvent.Close();
			this._tmr.Dispose();
		}
	}
}


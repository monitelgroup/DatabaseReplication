using System;
using System.Collections.Generic;

namespace replication
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Configurator config = new Configurator();
            ReplicationProcess testReplica = new ReplicationProcess(config);
			EventTimer tmr = new EventTimer(config.MainTimerValue, testReplica.OnTimedEvent);
			tmr.Start();
        }
	}
}
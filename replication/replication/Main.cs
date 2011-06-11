using System;

namespace replication
{
	class MainClass
	{
		public static void Main (string[] args)
		{

            Configurator config = new Configurator("ReplicationConfig.xml");

            ReplicationProcess testReplica = new ReplicationProcess(config);
            testReplica.OnStart();
            EventTimer tmr = new EventTimer(config.MainTimerValue, testReplica.OnTimedEvent);
			tmr.Start();
        }
	}
}

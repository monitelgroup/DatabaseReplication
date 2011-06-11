using System;

namespace replication
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Configurator config;
            
            if (args.Length == 0)
            {
                config = new Configurator("DefaultConfig.xml");
            }
            else
            {
                config = new Configurator(args[0]);
            }
            
            ReplicationProcess replication = new ReplicationProcess(config);
            replication.OnStart();

            EventTimer tmr = new EventTimer(config.MainTimerValue, replication.OnTimedEvent);
			tmr.Start();
        }
	}
}

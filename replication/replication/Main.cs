using System;

namespace replication
{
	class MainClass
	{
		public static void Main (string[] args)
		{

            Configurator config = new Configurator("ReplicationConfig.xml");

            //DBManager dbm = new DBManager(config.MasterServerName, "anton", "password", config.MasterDBName);
            //DBManager dbm = new DBManager(config.MasterServerName, config.SlaveDBName);
            //Console.WriteLine(dbm.IsObjectNotNull("Sales", "Shippers"));
            //dbm.CreateDB();
            
            ReplicationProcess testReplica = new ReplicationProcess(config);
            testReplica.OnStart();
            EventTimer tmr = new EventTimer(config.MainTimerValue, testReplica.OnTimedEvent);
			tmr.Start();
        }
	}
}

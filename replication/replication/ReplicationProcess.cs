using System;

namespace replication
{
	public class ReplicationProcess
	{
		
		private Configurator _config;
		
		public ReplicationProcess(Configurator config) {
			this._config = config;
		}
		
		public void StartReplication() {
			
		}
		
		public static void OnTimedEvent(Object stateInfo) {
        	Console.WriteLine("Event in TestTimer.");
    	}
		
	}
}
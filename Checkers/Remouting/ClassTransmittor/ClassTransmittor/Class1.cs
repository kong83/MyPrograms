using System;

namespace ClassTransmittor
{
	public class Transmittor : MarshalByRefObject
	{
		public Transmittor()
		{
			// Default no argument constructor
		}

		public override Object InitializeLifetimeService()
		{
			// Allow this object to live "forever"
			return null;
		}

		public delegate string GetTimeEventHandler();
		public event GetTimeEventHandler GetTimeEvent;
		

		public string GetTime()
		{				
			return GetTimeEvent();
		}

		public delegate string[] GetTimeArrayEventHandler(int cnt);
		public event GetTimeArrayEventHandler GetTimeArrayEvent;		

		public string[] GetTimeArray(int cnt)
		{
			return GetTimeArrayEvent(cnt);
		}

		public delegate int GetFromClientNumberEventHandler(int cnt);
		public event GetFromClientNumberEventHandler GetFromClientNumberEvent;

		public int GetFromClientNumber(int cnt)
		{
			return GetFromClientNumberEvent(cnt);
		}
	}
}

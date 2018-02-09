using System;

namespace FromClientClass
{
    public class FromClientTransmittor : MarshalByRefObject
    {
        public override Object InitializeLifetimeService()
        {
            // Allow this object to live "forever"
            return null;
        }

        // Предложение о ничьей
        public delegate bool ToServerRemiEventHandler();
        public event ToServerRemiEventHandler ToServerRemiEvent;

        public bool ToServerRemi()
        {
            return ToServerRemiEvent();
        }

        // Сообщение о сдаче партии
        public delegate void ToServerDeliverEventHandler();
        public event ToServerDeliverEventHandler ToServerDeliverEvent;

        public void ToServerDeliver()
        {
            ToServerDeliverEvent();
        }

        // Старт соединения (посылается IP-адрес клиента)
        public delegate void StartGameEventHandler(string ipClient);
        public event StartGameEventHandler StartGameEvent;

        public void StartGame(string ipClient)
        {
            StartGameEvent(ipClient);
        }

        // Сообщение о том, что клиент готов
        public delegate void ToServerReadyEventHandler(string timeAll, string addSec);
        public event ToServerReadyEventHandler ToServerReadyEvent;

        public void ToServerReady(string timeAll, string addSec)
        {
            ToServerReadyEvent(timeAll, addSec);
        }

        // Сообщение о том, что клиент отключился
        public delegate void ToServerDisableEventHandler();
        public event ToServerDisableEventHandler ToServerDisableEvent;

        public void ToServerDisable()
        {
            ToServerDisableEvent();
        }        

        // Отсылка очередного хода
        public delegate void ToServerStepEventHandler(int x, int y, int timeClient);
        public event ToServerStepEventHandler ToServerStepEvent;

        public void ToServerStep(int x, int y, int timeClient)
        {
            ToServerStepEvent(x, y, timeClient);
        }
    }
}

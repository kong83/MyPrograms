using System;

namespace FromServerClass
{
    public class FromServerTransmittor : MarshalByRefObject
    {
        public override Object InitializeLifetimeService()
        {
            // Allow this object to live "forever"
            return null;
        }

        // Предложение о ничьей
        public delegate bool ToClientRemiEventHandler();
        public event ToClientRemiEventHandler ToClientRemiEvent;

        public bool ToClientRemi()
        {
            return ToClientRemiEvent();
        }

        // Сообщение о сдаче партии
        public delegate void ToClientDeliverEventHandler();
        public event ToClientDeliverEventHandler ToClientDeliverEvent;

        public void ToClientDeliver()
        {
            ToClientDeliverEvent();
        }

        // Сообщение о том, что сервер готов
        public delegate void ToClientReadyEventHandler(string timeAll, string addSec);
        public event ToClientReadyEventHandler ToClientReadyEvent;

        public void ToClientReady(string timeAll, string addSec)
        {
            ToClientReadyEvent(timeAll, addSec);
        }

        // Сообщение о том, что сервер отключился
        public delegate void ToClientDisableEventHandler();
        public event ToClientDisableEventHandler ToClientDisableEvent;

        public void ToClientDisable()
        {
            ToClientDisableEvent();
        }        

        // Отсылка очередного хода
        public delegate void ToClientStepEventHandler(int x, int y, int timeServer);
        public event ToClientStepEventHandler ToClientStepEvent;

        public void ToClientStep(int x, int y, int timeServer)
        {
            ToClientStepEvent(x, y, timeServer);
        }
    }
}

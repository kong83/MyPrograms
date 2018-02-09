using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

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
        public delegate void ToClientReadyEventHandler(string timeAll, string addSec, ArrayList boardMas);
        public event ToClientReadyEventHandler ToClientReadyEvent;

        public void ToClientReady(string timeAll, string addSec, ArrayList boardMas)
        {
            ToClientReadyEvent(timeAll, addSec, boardMas);
        }

        // Сообщение о том, что клиент отключился
        public delegate void ToClientDisableEventHandler();
        public event ToClientDisableEventHandler ToClientDisableEvent;

        public void ToClientDisable()
        {
            ToClientDisableEvent();
        }

        // Просьба отменить ход
        public delegate bool ToClientBackStepEventHandler();
        public event ToClientBackStepEventHandler ToClientBackStepEvent;

        public bool ToClientBackStep()
        {
            return ToClientBackStepEvent();
        }

        // Отсылка очередного хода
        public delegate void ToClientStepEventHandler(int[] selectedCell, int x, int y, int timeServer);
        public event ToClientStepEventHandler ToClientStepEvent;

        public void ToClientStep(int[] selectedCell, int x, int y, int timeServer)
        {
            ToClientStepEvent(selectedCell, x, y, timeServer);
        }
    }
}

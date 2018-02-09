using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

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
        public delegate void ToServerReadyEventHandler(string timeAll, string addSec, ArrayList boardMas);
        public event ToServerReadyEventHandler ToServerReadyEvent;

        public void ToServerReady(string timeAll, string addSec, ArrayList boardMas)
        {
            ToServerReadyEvent(timeAll, addSec, boardMas);
        }

        // Сообщение о том, что клиент отключился
        public delegate void ToServerDisableEventHandler();
        public event ToServerDisableEventHandler ToServerDisableEvent;

        public void ToServerDisable()
        {
            ToServerDisableEvent();
        }

        // Просьба отменить ход
        public delegate bool ToServerBackStepEventHandler();
        public event ToServerBackStepEventHandler ToServerBackStepEvent;

        public bool ToServerBackStep()
        {
            return ToServerBackStepEvent();
        }

        // Отсылка очередного хода
        public delegate void ToServerStepEventHandler(int[] selectedCell, int x, int y, int timeClient);
        public event ToServerStepEventHandler ToServerStepEvent;

        public void ToServerStep(int[] selectedCell, int x, int y, int timeClient)
        {
            ToServerStepEvent(selectedCell, x, y, timeClient);
        }
    }
}

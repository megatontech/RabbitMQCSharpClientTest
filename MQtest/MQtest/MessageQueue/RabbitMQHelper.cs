using System;
using TJAP.Common.Tools.MessageQueue.Client;
using System.Linq;
using System.Collections.Generic;

namespace TJAP.Common.Tools.MessageQueue
{
    public static class RabbitMQHelper
    {
        public static void Send()
        {
            for (var i = 1; i < 10000; i++)
            {
                object originObject = Guid.NewGuid().ToString() + "AUTO";
                var sendMessage = EventMessageFactory.CreateEventMessageInstance(originObject, "AUTO");
                AutoMQClient.Instance.TriggerEventMessage(sendMessage, "AUTO", "AUTO");
                Console.WriteLine(i);
            }
            //for (var i = 1; i < 10000; i++)
            //{
            //    object originObject = Guid.NewGuid().ToString() + "CALC";
            //    var sendMessage = EventMessageFactory.CreateEventMessageInstance(originObject, "CALC");
            //    CalcMQClient.Instance.TriggerEventMessage(sendMessage, "CALC", "CALC");
            //    Console.WriteLine(i);
            //}
        }

        public static void Listening()
        {
            AutoMQClient.Instance.ActionEventMessage += mqAutoMQClientClient_ActionEventMessage;
            AutoMQClient.Instance.OnListening();
            //CalcMQClient.Instance.ActionEventMessage += mqCALCMQClientClient_ActionEventMessage;
            //CalcMQClient.Instance.OnListening();
        }

        private static void mqAutoMQClientClient_ActionEventMessage(EventMessageResult result)
        {
            if (result.EventMessageBytes.EventMessageMarkcode == "AUTO")
            {
                var message = MessageSerializerFactory.CreateMessageSerializerInstance("json")
                        .DeserializeByte<object>(result.MessageBytes);
                result.IsOperationOk = true; //处理成功
                Console.WriteLine(message.ToString());
            }
        }
        private static void mqCALCMQClientClient_ActionEventMessage(EventMessageResult result)
        {
            if (result.EventMessageBytes.EventMessageMarkcode == "CALC")
            {
                var message = MessageSerializerFactory.CreateMessageSerializerInstance("json")
                        .DeserializeByte<object>(result.MessageBytes);
                result.IsOperationOk = true; //处理成功
                Console.WriteLine(message.ToString());
            }
        }
    }
}
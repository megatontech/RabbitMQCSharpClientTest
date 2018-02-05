namespace TJAP.Common.Tools.MessageQueue
{
    /// <summary>
    /// IMessageSerializer 创建工厂。
    /// </summary>
    public class MessageSerializerFactory
    {
        /// <summary>
        /// 创建一个消息序列化组件。
        /// </summary>
        /// <returns></returns>
        public static IMessageSerializer CreateMessageSerializerInstance(string formatType)
        {
            IMessageSerializer messageSerializer = null;
            switch (formatType)
            {
                case "json":
                    messageSerializer = new MessageJsonSerializer();
                    break;

                case "xml":
                    messageSerializer = new MessageXmlSerializer();
                    break;

                default:
                    messageSerializer = new MessageJsonSerializer();
                    break;
            }
            return messageSerializer;
        }
    }
}
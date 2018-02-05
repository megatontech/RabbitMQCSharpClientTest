using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace TJAP.Common.Tools.MessageQueue
{
    /// <summary>
    /// 面向XML的消息序列化。
    /// </summary>
    public class MessageJsonSerializer : IMessageSerializer
    {
        /// <summary>
        /// 序列化成bytes。
        /// </summary>
        /// <typeparam name="T">消息的类型。</typeparam>
        /// <param name="message">消息的实例。</param>
        /// <returns></returns>
        public byte[] SerializerBytes<T>(T message) where T : class, new()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }

        /// <summary>
        /// 序列化消息为Json字符串。
        /// </summary>
        /// <param name="message">消息类型</param>
        /// <typeparam name="T">消息实例</typeparam>
        /// <returns></returns>
        public string SerializerString<T>(T message) where T : class, new()
        {
            return JsonConvert.SerializeObject(message);
        }

        /// <summary>
        /// 反序列化消息。
        /// </summary>
        /// <typeparam name="T">消息的类型。</typeparam>
        /// <param name="bytes">bytes。</param>
        /// <returns></returns>
        public T DeserializeByte<T>(byte[] bytes) where T : class, new()
        {
            using (var stream = new MemoryStream(bytes))
            {
                byte[] dataBytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(dataBytes, 0, (int)stream.Length);
                string jsonStr = Encoding.UTF8.GetString(dataBytes);
                T result = (T)JsonConvert.DeserializeObject<T>(jsonStr);
                return result;
            }
        }
    }
}
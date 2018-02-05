using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TJAP.Common.Tools.MessageQueue;
using TJAP.Common.Tools.MessageQueue.Client;
using TJAP.Common.Tools.MessageQueue.Common;

namespace MQtest
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 监听线程
        /// </summary>
        public Thread ListeningThread;

        public EventingBasicConsumer eventingBasicConsumer;

        /// <summary>
        /// RabbitMqClient 数据上下文。
        /// </summary>
        public RabbitMqClientContext Context { get; set; }

        /// <summary>
        /// 事件激活委托实例。
        /// </summary>
        public ActionEvent _actionMessage;

        /// <summary>
        /// 当侦听的队列中有消息到达时触发的执行事件。
        /// </summary>
        public event ActionEvent ActionEventMessage
        {
            add
            {
                if (_actionMessage.IsNull())
                    _actionMessage += value;
            }
            remove
            {
                if (_actionMessage.IsNotNull())
                    _actionMessage -= value;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            object originObject = Guid.NewGuid().ToString() + "AUTO";
            var sendMessage = EventMessageFactory.CreateEventMessageInstance(originObject, "AUTO");
            AutoMQClient.Instance.TriggerEventMessage(sendMessage, "AUTO", "AUTO");
            //var conn = new ConnectionFactory() { HostName = "127.0.0.1" };
            //using (var connmodel = conn.CreateConnection())
            //{
            //    using (var channel = connmodel.CreateModel())
            //    {
            //        channel.QueueDeclare(queue: "CALC",
            //                     durable: false,
            //                     exclusive: false,
            //                     autoDelete: false,
            //                     arguments: null);

            //        string messageGUID = Guid.NewGuid().ToString();
            //        textBox1.Text = messageGUID;
            //        var body = Encoding.UTF8.GetBytes(messageGUID);
            //        channel.BasicPublish(exchange: "",
            //                             routingKey: "CALC",
            //                             basicProperties: null,
            //                             body: body);
            //        //MessageBox.Show("send"+ messageGUID);
            //    }
            //}
        }

        /// <summary>
        /// receive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Context = new RabbitMqClientContext();

            OnListening();
            //string msg = "";
            //var conn = new ConnectionFactory() { HostName = "192.168.1.95" };
            //using (var connmodel = conn.CreateConnection())
            //using (var channel = connmodel.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "HGCONVERT",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    var consumer = new EventingBasicConsumer(channel);
            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body;
            //        var message = Encoding.UTF8.GetString(body);
            //        msg = message;
            //        MessageBox.Show("receive" + message);
            //        //label1.Text = message.ToString();
            //    };
            //   // channel.BasicConsume(queue: "CALC",
            //    //                     noAck: true,
            //    //                     consumer: consumer);
            //}
        }
        /// <summary>
        /// 开始消息队列的默认监听。
        /// </summary>
        public void OnListening()
        {
            Task.Factory.StartNew(ListenInit);
        }

        public void OnThreadListening()
        {
            ListeningThread = new Thread(() => { LoopListening(); });
            ListeningThread.Start();
        }

        public void LoopListening()
        {
            //while (true)
            {
                ListenInit();
            }
        }

        /// <summary>
        /// 侦听初始化。
        /// </summary>
        public virtual void ListenInit()
        {
            try
            {
                string message = "";
                Context.ListenConnection = this.Context.ListenConnection;//获取连接
                if (Context.ListenConnection == null || !Context.ListenConnection.IsOpen)
                {
                    Context.ListenConnection = RabbitMqClientFactory.CreateListenConnection(new TJAP.Common.Tools.MessageQueue.Config.MqConfigDom {  MqListenHost="192.168.1.95", MqListenQueueName= "HGCONVERT" , MqUserName ="guest",MqPassword="guest"});
                }
                Context.ListenConnection.ConnectionBlocked += (o, e) =>
                {
                    //连接阻塞
                };
                Context.ListenConnection.ConnectionUnblocked += (o, e) =>
                {
                    //连接解除阻塞
                };
                Context.ListenConnection.CallbackException += (o, e) =>
                {
                    //回调异常
                };
                Context.ListenConnection.ConnectionShutdown += (o, e) =>
                {
                    //关闭连接
                };
                Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection); //获取通道
                Context.ListenChannel.QueueDeclare("HGCONVERT", false, false, false, null);
                eventingBasicConsumer = new EventingBasicConsumer(Context.ListenChannel); //创建事件驱动的消费者类型
                eventingBasicConsumer.Received += (model, ea) =>
                {
                    try
                    {
                       var body = ea.Body;
                       message = Encoding.UTF8.GetString(body);//获取消息返回对象
                       //未能消费此消息，重新放入队列头
                       Context.ListenChannel.BasicReject(ea.DeliveryTag, true);
                    }
                    catch (Exception exception)
                    {
                    }
                };
                Context.ListenChannel.BasicQos(0, 1, false); //一次只获取一个消息进行消费
            }
            catch (Exception ex)
            {
            }
        }

    }
}
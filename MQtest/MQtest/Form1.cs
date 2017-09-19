using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Windows.Forms;

namespace MQtest
{
    public partial class Form1 : Form
    {
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
            var conn = new ConnectionFactory() { HostName = "127.0.0.1" };
            using (var connmodel = conn.CreateConnection())
            {
                using (var channel = connmodel.CreateModel())
                {
                    channel.QueueDeclare(queue: "MyChannel",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string messageGUID = Guid.NewGuid().ToString();
                    textBox1.Text = messageGUID;
                    var body = Encoding.UTF8.GetBytes(messageGUID);
                    channel.BasicPublish(exchange: "",
                                         routingKey: "MyChannel",
                                         basicProperties: null,
                                         body: body);
                    MessageBox.Show("send"+ messageGUID);
                }
            }
        }

        /// <summary>
        /// receive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var conn = new ConnectionFactory() { HostName = "127.0.0.1" };
            using (var connmodel = conn.CreateConnection())
            using (var channel = connmodel.CreateModel())
            {
                channel.QueueDeclare(queue: "MyChannel",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    MessageBox.Show("receive" + message);
                    //label1.Text = message.ToString();
                };
                channel.BasicConsume(queue: "MyChannel",
                                     noAck: true,
                                     consumer: consumer);
            }
        }
    }
}
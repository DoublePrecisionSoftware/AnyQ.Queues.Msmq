using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AnyQ.Queues.Msmq.Tests {
    [TestClass]
    public class MsmqMessageQueueTests {
        private const string queueId = @".\private$\AnyQ-Queues-Msmq-Tests";

        private MsmqMessageQueue _sut;
        private MessageQueue _backingQueue;

        [TestInitialize]
        public void Setup() {
            if (MessageQueue.Exists(queueId)) {
                MessageQueue.Delete(queueId);
            }

            //var acl = new System.Messaging.AccessControlList {
            //    new System.Messaging.AccessControlEntry(
            //    new System.Messaging.Trustee(Environment.UserName, Environment.MachineName),
            //    System.Messaging.GenericAccessRights.All,
            //    System.Messaging.StandardAccessRights.All,
            //    System.Messaging.AccessControlEntryType.Allow)
            //};

            _sut = new MsmqMessageQueue(new QueueCreationOptions {
                QueueId = queueId,
                QueueName = "AnyQ.Queues.Msmq Test Queue"
            }, false);

            _backingQueue = new MessageQueue(queueId);
        }

        [TestCleanup]
        public void Cleanup() {
            if (MessageQueue.Exists(queueId)) {
                MessageQueue.Delete(queueId);
            }
        }

        private IMessage SendTestMessage() {
            return _sut.Send(new MsmqMessage(new MemoryStream(Encoding.UTF8.GetBytes("test")), "Test Message"));
        }

        [TestMethod]
        public void InitializeQueue_Creates_Queue() {
            Assert.IsTrue(MessageQueue.Exists(queueId));
        }

        [TestMethod]
        public void Send_Adds_Message_To_Queue() {
            var msg = SendTestMessage();
            var queueMsg = _backingQueue.Peek();

            Assert.IsNotNull(msg);
            Assert.IsNotNull(queueMsg);
            Assert.AreEqual(queueMsg.Id, msg.Id);
            Assert.AreEqual(queueMsg.Label, msg.Label);
            //Assert.AreEqual(queueMsg.BodyStream, msg.BodyStream); // TODO
        }

        [TestMethod]
        [Timeout(2000)]
        public void Receive_Fires_ReceivedMessage() {
            var tcs = new TaskCompletionSource<ReceivedMessageEventArgs>();

            _sut.ReceivedMessage += (s, e) => {
                tcs.SetResult(e);
            };

            SendTestMessage();

            _sut.Receive();

            tcs.Task.Wait();

            Assert.IsNotNull(tcs.Task.Result.Message);
        }

        [TestMethod]
        [Timeout(2000)]
        public void Receive_Gets_Specific_Message() {
            var tcs = new TaskCompletionSource<ReceivedMessageEventArgs>();

            _sut.ReceivedMessage += (s, e) => {
                tcs.SetResult(e);
            };

            var first = SendTestMessage();
            SendTestMessage();

            _sut.Receive(first.Id);

            tcs.Task.Wait();

            Assert.AreEqual(tcs.Task.Result.Message.Id, first.Id);
        }
    }
}
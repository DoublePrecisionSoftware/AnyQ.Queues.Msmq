using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnyQ.Queues.Msmq.Tests {
    [TestClass]
    public class MsmqMessageQueueTests {
        private const string queueId = @".\private$\AnyQ.Queues.Msmq.Tests";

        private MsmqMessageQueue _sut;

        [TestInitialize]
        public void Setup() {
            System.Messaging.MessageQueue.Delete(queueId);
            _sut = new MsmqMessageQueue(new QueueCreationOptions {
                QueueId = queueId,
                QueueName = "AnyQ.Queues.Msmq Test Queue"
            }, false);
        }

        [TestMethod]
        public void InitializeQueue_Creates_Queue() {
            Assert.IsTrue(System.Messaging.MessageQueue.Exists(queueId));
        }
    }
}
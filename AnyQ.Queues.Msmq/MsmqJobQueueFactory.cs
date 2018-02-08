using AnyQ.Formatters;
using AnyQ.Jobs;
using System;

namespace AnyQ.Queues.Msmq {
    public class MsmqJobQueueFactory : IJobQueueFactory {
        private readonly MsmqMessageQueueFactory _queueFactory;
        private readonly MsmqMessageFactory _messageFactory;
        private readonly IPayloadFormatter _payloadFormatter;
        private readonly IRequestSerializer _requestSerializer;

        public MsmqJobQueueFactory(
            IPayloadFormatter payloadFormatter, 
            IRequestSerializer requestSerializer,
            System.Messaging.AccessControlList accessControlList = null) {

            _queueFactory = new MsmqMessageQueueFactory(accessControlList);
            _messageFactory = new MsmqMessageFactory();
            _payloadFormatter = payloadFormatter;
            _requestSerializer = requestSerializer;
        }

        public JobQueue Create(HandlerConfiguration handlerConfiguration) {
            return new JobQueue(_queueFactory, _messageFactory, _payloadFormatter, _requestSerializer, handlerConfiguration);
        }
    }
}

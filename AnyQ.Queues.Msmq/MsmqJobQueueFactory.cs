using AnyQ.Formatters;
using AnyQ.Jobs;

namespace AnyQ.Queues.Msmq {
    /// <summary>
    /// Provides a simple abstraction over <see cref="MsmqMessageQueueFactory"/> and
    /// <see cref="MsmqMessageFactory"/> for passing to a <see cref="JobQueueListener"/>
    /// </summary>
    public class MsmqJobQueueFactory : IJobQueueFactory {
        private readonly MsmqMessageQueueFactory _queueFactory;
        private readonly MsmqMessageFactory _messageFactory;
        private readonly IPayloadFormatter _payloadFormatter;
        private readonly IRequestSerializer _requestSerializer;

        /// <summary>
        /// Initialize a new instance of the <see cref="MsmqJobQueueFactory"/>
        /// </summary>
        /// <param name="payloadFormatter">Formats object payloads added to queue</param>
        /// <param name="requestSerializer">Deserializes payload coming from the queue</param>
        /// <param name="accessControlList">Specifies users and their access to the queue</param>
        public MsmqJobQueueFactory(
            IPayloadFormatter payloadFormatter, 
            IRequestSerializer requestSerializer,
            System.Messaging.AccessControlList accessControlList = null) {

            _queueFactory = new MsmqMessageQueueFactory(accessControlList);
            _messageFactory = new MsmqMessageFactory();
            _payloadFormatter = payloadFormatter;
            _requestSerializer = requestSerializer;
        }

        /// <summary>
        /// Creates a new <see cref="JobQueue"/> with the proper dependencies
        /// </summary>
        /// <param name="handlerConfiguration">Configuration for the handler handling this queue</param>
        public JobQueue Create(HandlerConfiguration handlerConfiguration) {
            return new JobQueue(_queueFactory, _messageFactory, _payloadFormatter, _requestSerializer, handlerConfiguration);
        }
    }
}

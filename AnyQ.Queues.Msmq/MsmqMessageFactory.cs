using System.IO;
using System.Text;

namespace AnyQ.Queues.Msmq {
    /// <summary>
    /// Provides methods for creating <see cref="MsmqMessage"/> objects
    /// </summary>
    public class MsmqMessageFactory : IMessageFactory {
        /// <summary>
        /// Creates a new <see cref="MsmqMessage"/> from the specified stream
        /// </summary>
        /// <param name="bodyStream"><see cref="Stream"/> containing the message data</param>
        /// <param name="label">Human-readable label for the message</param>
        public IMessage Create(byte[] body, Encoding encoding, string label) {
            return new MsmqMessage(new MemoryStream(body), encoding, label);
        }
    }
}

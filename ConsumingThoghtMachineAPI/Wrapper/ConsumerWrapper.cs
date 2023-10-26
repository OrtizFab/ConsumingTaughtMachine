using Confluent.Kafka;

namespace ConsumingThoghtMachineAPI.Wrapper
{
    public class ConsumerWrapper
    {
        private string _topicName;
        private ConsumerConfig _consumerConfig;
        private Consumer<string, string> _consumer;
        private static readonly Random rand = new Random();
        public ConsumerWrapper(ConsumerConfig config, string topicName)
        {
            this._topicName = topicName;
            this._consumerConfig = config;
            this._consumer = new Consumer<string, string>(this._consumerConfig);
            this._consumer.Subscribe(topicName);
        }
        public string readMessage()
        {
            try
            {
                var consumeResult = this._consumer.Consume();
                //if (consumeResult == null)
                //{
                //    consumeResult.Value = "{}";
                //}
                return consumeResult.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}

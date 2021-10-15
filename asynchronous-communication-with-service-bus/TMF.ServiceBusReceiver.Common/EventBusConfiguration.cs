namespace TMF.ServiceBusReceiver.Common
{
    public class EventBusConfiguration
    {
        public string ListenAndSendConnectionString { get; set; }
        public string TopicName { get; set; }
        public string Subscription { get; set; }
        public string QueueName { get; set; }
    }
}

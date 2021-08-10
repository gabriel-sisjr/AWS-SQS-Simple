using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        private readonly static string _QueueUrl = "https://sqs.us-east-1.amazonaws.com/205886172740/EstudoSQS";
        static async Task Main(string[] args)
        {
            var client = new AmazonSQSClient(RegionEndpoint.USEast1);
            var request = new ReceiveMessageRequest()
            {
                QueueUrl = _QueueUrl
            };

            // Waiting for the Producer produce the messages.
            await Task.Delay(2000);

            var hasMessage = true;
            while (hasMessage)
            {
                var response = await client.ReceiveMessageAsync(request);
                response.Messages.ForEach(msg =>
                {
                    Console.WriteLine(msg.Body);
                    client.DeleteMessageAsync(_QueueUrl, msg.ReceiptHandle);
                });

                hasMessage = response.Messages.Count != 0;
            }
        }
    }
}

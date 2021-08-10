using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Threading.Tasks;

namespace Producer
{
    class Program
    {
        private readonly static string _QueueUrl = "https://sqs.us-east-1.amazonaws.com/205886172740/EstudoSQS";
        static async Task Main(string[] args)
        {
            var client = new AmazonSQSClient(RegionEndpoint.USEast1);

            var request = new SendMessageRequest()
            {
                QueueUrl = _QueueUrl
            };

            for (var i = 0; i < 10; i++)
            {
                request.MessageBody = $"Mensagem numero: {i}";
                await client.SendMessageAsync(request);
            }
        }
    }
}

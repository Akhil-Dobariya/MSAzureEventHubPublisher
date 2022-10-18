using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using System.Threading.Tasks;

namespace MSAzureEventHubPublisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "PrimaryOrSecondary Connection String";
            string eventHubName = "EventHub Name";

            try
            {
                await using (EventHubProducerClient producer = new EventHubProducerClient(connectionString, eventHubName))
                {
                    Task t1 = Task.Run(async () =>
                    {
                        EventDataBatch eventBatch = await producer.CreateBatchAsync();

                        for (int i = 0; i <= 20; i++)
                        {
                            eventBatch.TryAdd(new EventData(new BinaryData($"T1 Test {i}")));

                            await producer.SendAsync(eventBatch);
                            Console.WriteLine($"sent: T1 Test {i}");
                        }
                    });

                    Task t2 = Task.Run(async () =>
                    {
                        for (int i = 21; i <= 40; i++)
                        {
                            EventDataBatch eventBatch = await producer.CreateBatchAsync();
                            eventBatch.TryAdd(new EventData(new BinaryData($"T2 Test {i}")));

                            await producer.SendAsync(eventBatch);

                        }
                    });

                    Task t3 = Task.Run(async () =>
                    {
                        EventDataBatch eventBatch = await producer.CreateBatchAsync();

                        for (int i = 41; i <= 60; i++)
                        {
                            eventBatch.TryAdd(new EventData(new BinaryData($"T1 Test {i}")));

                            await producer.SendAsync(eventBatch);
                            Console.WriteLine($"sent: T3 Test {i}");
                        }
                    });

                    Task t4 = Task.Run(async () =>
                    {
                        EventDataBatch eventBatch = await producer.CreateBatchAsync();

                        for (int i = 61; i <= 80; i++)
                        {
                            eventBatch.TryAdd(new EventData(new BinaryData($"T1 Test {i}")));

                            await producer.SendAsync(eventBatch);
                            Console.WriteLine($"sent: T4 Test {i}");
                        }
                    });

                    t1.Wait();
                    t2.Wait();
                    t3.Wait();
                    t4.Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception {ex.ToString()}");
            }

            Console.ReadLine();

        }
    }
}

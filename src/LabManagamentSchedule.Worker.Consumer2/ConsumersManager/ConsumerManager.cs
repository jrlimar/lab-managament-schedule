using LabManagamentSchedule.Worker.Consumer2.Consumers;
using LabManagamentSchedule.Worker.Consumer2.Consumers.Interfaces;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Worker.Consumer2.ConsumersManager
{
    public class ConsumerManager : IConsumerManager
    {
        private readonly ICheckExamConsumer checkExamConsumer;
        private readonly ITesteConsumer testeConsumer;

        public ConsumerManager(ICheckExamConsumer checkExamConsumer, ITesteConsumer testeConsumer)
        {
            this.checkExamConsumer = checkExamConsumer;
            this.testeConsumer = testeConsumer;
        }

        public async Task SetConsumers()
        {
            await checkExamConsumer.Received();
            await testeConsumer.Received();
        }
    }
}

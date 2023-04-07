namespace Web.CounterQueueExample.Services
{
    public abstract class CounterOperation : ICounterOperation
    {
        private readonly string _operationName;

        private static readonly ICounterOperation[] _operations = new ICounterOperation[]
        {
            new IncrementOperation(),
            new DecrementOperation()
        };

        public static readonly string[] Operations = _operations.Select(o => o.ToString()).ToArray();

        public static ICounterOperation GetOperation(string operationName)
        {
            var operationIndex = Array.IndexOf(Operations, operationName);

            if (operationIndex < 0)
                throw new InvalidCounterOperationException(operationName);

            return _operations[operationIndex];
        }

        public CounterOperation(string operationName) =>
            _operationName = operationName;

        public abstract int PerformOperation(int value);

        public override string ToString() => _operationName;
    }

    public class IncrementOperation : CounterOperation
    {
        public IncrementOperation() : base("Increment")
        { }

        public override int PerformOperation(int value) => ++value;
    }

    public class DecrementOperation : CounterOperation
    {
        public DecrementOperation() : base("Decrement")
        { }

        public override int PerformOperation(int value) => -- value ;
    }
}

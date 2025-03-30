namespace Client.ResponseHandler
{
    public interface IProgressMessage<TResult, TProgress>
    {
        public bool IsProgress { get; }
        public bool IsResult { get; }
        public TProgress Progress { get; }
        public TResult Result { get; }
    }
}

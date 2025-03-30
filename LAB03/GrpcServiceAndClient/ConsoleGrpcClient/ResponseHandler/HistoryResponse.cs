using Client.ResponseHandler;

namespace Progress
{
    // Use a partial class to add an interface to the generated HistoryResponse type
    public partial class HistoryResponse : IProgressMessage<HistoryResult, int>
    {
        bool IProgressMessage<HistoryResult, int>.IsProgress => ResponseTypeCase == ResponseTypeOneofCase.Progress;

        bool IProgressMessage<HistoryResult, int>.IsResult => ResponseTypeCase == ResponseTypeOneofCase.Result;
    }
}
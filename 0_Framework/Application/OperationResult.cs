namespace _0_Framework.Application
{
    public class OperationResult
    {
        public string Message { get; set; }

        public bool IsSuccessful { get; set; }

        public bool IsNeedToLoginAgain { get; set; }

        public OperationResult()
        {
            IsSuccessful = false;
            IsNeedToLoginAgain = false;
        }

        public OperationResult Succeeded(string message ="عملیات با موفقیت انجام شد")
        {
            IsSuccessful = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message = "")
        {
            IsSuccessful = false;
            Message = message;
            return this;
        }

        public OperationResult SucceededNeedToLoginAgain(string message = "عملیات با موفقیت انجام شد")
        {
            IsSuccessful = true;
            IsNeedToLoginAgain = true;
            Message = message;
            return this;
        }
    }
}

namespace PicPaySimplified.Models.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; private set; }
        public T? Value { get; private set; }

        private Result(bool isSuccess, T? value, string? errorMessage)
        {
            if (isSuccess && errorMessage != null)
                throw new InvalidOperationException("Resultados bem-sucedidos não devem conter mensagens de erro.");
            if (!isSuccess && value != null)
                throw new InvalidOperationException("Resultados com falha não devem conter valor.");

            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null);
        public static Result<T> Failure(string errorMessage) => new Result<T>(false, default, errorMessage);
    }
}

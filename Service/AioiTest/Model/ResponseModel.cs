namespace AioiTest.Model
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public static ResponseModel<T> Success(T data) => new ResponseModel<T> { IsSuccess = true, Data = data };
        public static ResponseModel<T> Failure(string error) => new ResponseModel<T> { IsSuccess = false, Error = error };
    }
}

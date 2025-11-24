namespace Application.Common.Models
{
    public class Response<T>
    {
        public Response(T data)
        {
            this.Data = data;
        }

        public Response(User userResult)
        {
            UserResult = userResult;
        }

        public Response(string error)
        {
            this.Error = error;
        }

        public T Data { get; }
        public User UserResult { get; }
        public string Error { get; }
        public bool IsSuccess => string.IsNullOrEmpty(this.Error);
    }
}
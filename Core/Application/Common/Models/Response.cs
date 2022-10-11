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

        public T Data { get; }
        public User UserResult { get; }
    }
}
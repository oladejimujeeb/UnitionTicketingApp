namespace UnitionTicketingApp.DTO
{
    public class BaseResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; } = "Success";
        public T Data { get; set; }
    }
    public class BaseResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; } = "Success";
    }
}

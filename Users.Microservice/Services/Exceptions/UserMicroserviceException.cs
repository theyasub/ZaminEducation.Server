namespace Users.Microservice.Services.Exceptions
{
    public class UserMicroserviceException : Exception
    {
        public int Code { get; set; }
        public UserMicroserviceException(int code, string message)
            : base(message)
        {
            this.Code = code;
        }
    }
}

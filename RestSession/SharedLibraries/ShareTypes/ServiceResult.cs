namespace SharedLibraries.ShareTypes
{
    public class ServiceStatus
    {
        // Default to Fail status
        public ServiceStatus()
        {
            Success = false;
            Message = "Service Call failed";
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ServiceResult<T>
    {
        public ServiceResult()
        {
            Status = new ServiceStatus();
        }

        public ServiceStatus Status { get; set; }
        public T Result { get; set; }
    }
}

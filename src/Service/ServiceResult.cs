namespace Stag.Service
{
    public class ServiceResult
    {
        public ServiceResult(bool success, string description)
        {
            this.Success = success;
            this.Description = description;
        }

        public bool Success { get; private set; }
        public string Description { get; private set; }
    }
}
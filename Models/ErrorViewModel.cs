namespace ST10372065.Models
{
    public class ErrorViewModel
    {
        public string? Message { get; set; } // Add this
        public Exception? Exception { get; set; } // Add this
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

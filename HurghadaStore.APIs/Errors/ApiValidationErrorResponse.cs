namespace HurghadaStore.APIs.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse()
            :base(404)
        {
            Errors = new List<string>();
        }
    }
}

namespace ForceGet.Application.Interfaces
{
    public interface IApiNinjasService
    {
        public Task<string> ResponseFromRequest(string requestUri);
    }
}

namespace TemplateSpartaneApp.Abstractions
{
    public class ResponseBase<T>
    {
        public T Response { get; set; }
        public TypeReponse Status { get; set; }
    }

    public enum TypeReponse
    {
        Ok,
        Error,
        ErroConnectivity
    }
}

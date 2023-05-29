namespace QueryCEP.API.Dtos
{
    public record ApiCepResponse
    (
        int status,
        string code,
        string state,
        string city,
        string district,
        string address
    );
}
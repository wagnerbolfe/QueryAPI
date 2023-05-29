namespace QueryCEP.API.Dtos
{
    public record AwesomeApiResponse
    (
        string cep,
        string address_type,
        string address_name,
        string address,
        string district,
        string state,
        string city,
        string lat,
        string lng,
        string ddd,
        string city_ibge
    );
}
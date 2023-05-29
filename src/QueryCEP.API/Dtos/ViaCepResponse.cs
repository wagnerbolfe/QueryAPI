namespace QueryCEP.API.Dtos
{
    public record ViaCepResponse
    (
        string cep,
        string logradouro,
        string complemento,
        string bairro,
        string localidade,
        string uf,
        string ibge,
        string gia,
        string ddd,
        string siafi
    );
}
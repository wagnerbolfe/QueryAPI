using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QueryCEP.API.Dtos;
using QueryCEP.API.Entities;

namespace QueryCEP.API.Services
{
    public class InfoService : IInfoService
    {
        private readonly ILogger<InfoService> _logger;

        public InfoService(ILogger<InfoService> logger)
        {
            _logger = logger;
        }

        public async Task<Cep> ProcessInfo(string cep)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"https://viacep.com.br/ws/{FormatCep(cep)}/json/");
                    response.EnsureSuccessStatusCode();
                    _logger.LogInformation($"(ViaCepAPI status is {response.StatusCode})");

                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonSerializer.Deserialize<ViaCepResponse>(content,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    return new Cep
                    {
                        CepNumber = info.cep,
                        Address = info.logradouro,
                        City = info.localidade,
                        State = info.uf,
                        District = info.bairro,
                        DDD = info.ddd,
                        Ibge = info.ibge,
                        Siafi = info.siafi
                    };
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error consuming ViaCepAPI: {ex.Message}");
                }

                try
                {
                    var response = await client.GetAsync($"https://cdn.apicep.com/file/apicep/{FormatCep(cep)}.json");
                    response.EnsureSuccessStatusCode();
                    _logger.LogInformation($"(ApiCep status is {response.StatusCode})");

                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonSerializer.Deserialize<ApiCepResponse>(content,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    return new Cep
                    {
                        CepNumber = info.code,
                        Address = info.address,
                        City = info.city,
                        State = info.state,
                        District = info.district
                    };
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error consuming ApiCepAPI: {ex.Message}");
                }

                try
                {
                    var response = await client.GetAsync($"https://cep.awesomeapi.com.br/json/{FormatCep(cep)}");
                    response.EnsureSuccessStatusCode();
                    _logger.LogInformation($"(AwesomeApi status is {response.StatusCode})");

                    var content = await response.Content.ReadAsStringAsync();
                    var info = JsonSerializer.Deserialize<AwesomeApiResponse>(content,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        });
                    return new Cep
                    {
                        CepNumber = info.cep,
                        Address = info.address,
                        City = info.city,
                        State = info.state,
                        District = info.district,
                        DDD = info.ddd
                    };
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error consuming AwesomeAPI: {ex.Message}");
                    throw;
                }
            }
        }

        private string FormatCep(string cep)
        {
            var regExp = new Regex(@"^\d{5}-\d{3}");
            var regExp2 = new Regex(@"^\d{8}");

            if (regExp2.IsMatch(cep))
            {
                return Convert.ToUInt64(cep).ToString(@"00000\-000");
            }

            if (regExp.IsMatch(cep))
            {
                return cep;
            }
            else
            {
                throw new Exception(string.Format("Unable to use this CEP {0}", cep));
            }

        }
    }
}

using Microsoft.SemanticKernel;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel;

public class MyApiPlugin
{
    private readonly HttpClient _httpClient;

    public MyApiPlugin(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [KernelFunction]
    [Description("Give me the company information based on Schufa data")]
    public async Task<string> GetDataFromSchufaAsync(
        [Description("company name for Schufa")] string companyName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://fake-check-app.azurewebsites.net/api/schufa-tool?companyName={companyName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"API request failed: {ex.Message}", ex);
        }
    }

    [KernelFunction]
    [Description("Give me the company information based on European Sanction data")]
    public async Task<string> GetDataFromSanctionsAsync(
        [Description("company name for sanctions")] string companyName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://sancrtioneu.azurewebsites.net/api/sanctions-tool?NameAlias_WholeName={companyName}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"API request failed: {ex.Message}", ex);
        }
    }

    [KernelFunction]
    [Description("Give me the company score based on Schufa data and European Sanction data")]
    public async Task<string> GenerateSummaryAsync(
        [Description("company information")] string text)
    {
        // Implement your prompt logic here
        var prompt = $"Create a score from 1 to 10 for this: {text}";
        return await Task.FromResult(prompt);
    }
}
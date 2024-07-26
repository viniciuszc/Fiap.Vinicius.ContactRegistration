using Domain.Entity;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;

namespace IntegrationTest;

public class UnitTest1 : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public UnitTest1(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetService<SqlServerDbContext>();
            db.Users.Add(new User("teste", "teste@teste.com.br", "$2a$11$SgmG8e.TWwSNZ6GxBFWUIegcmvHr0J8/850ei6cvBoLO5yOI0SnOC"));

            var ddd = new DDD { Code = "011", Region = "Sudeste", UF = "SP" };
            db.DDDs.Add(ddd);

            var contact = new Contact(1, "Contato A", "(011)96278-0713", "teste@gmail.com");
            contact.DDD = ddd;
            db.Contacts.Add(contact);

            db.SaveChanges();
        }
    }

    [Fact]
    public async Task Test1()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("api/contact");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<Contact>>(content);

        result.Should().NotBeNull();
        result.Should().ContainEquivalentOf(new Contact(1, 1, "Contato A", "(011)96278-0713", "teste@gmail.com"));
    }
}
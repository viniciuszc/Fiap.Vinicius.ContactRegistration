using System.Text.Json.Serialization;

namespace Application.UseCase.Contacts.UpdateContact;

public class UpdateContactInput
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

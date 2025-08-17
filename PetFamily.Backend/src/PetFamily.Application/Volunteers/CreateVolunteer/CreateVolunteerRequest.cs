namespace PetFamily.Application.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    string firstname, string surname, string patronymic,
    string phone, 
    string email, 
    string nameReq, string descriptionReq, string descriptionTransferReq,
    string description, int experience)
{
    
}
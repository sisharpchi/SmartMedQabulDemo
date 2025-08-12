using Application.Dtos;

namespace Application.Abstractions.Services;

public interface IPatientService
{
    Task UpdatePatientAsync(PatientUpdateDto patient, long patientId);
}

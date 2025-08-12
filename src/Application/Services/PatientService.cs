using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;

namespace Application.Services;

public class PatientService(IPatientRepository _repo) : IPatientService
{
    public async Task UpdatePatientAsync(PatientUpdateDto patient,long patientId)
    {
        var patientEntity = await _repo.GetPatientByIdAsync(patientId);
        patientEntity.DateOfBirth = patient.DateOfBirth;
        patientEntity.Gender = patient.Gender;
        await _repo.UpdatePatientAsync(patientEntity);
    }
}

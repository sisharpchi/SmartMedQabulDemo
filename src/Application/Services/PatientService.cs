using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Mappers;

namespace Application.Services;

public class PatientService(IPatientRepository _repo) : IPatientService
{
    public async Task<ICollection<PatientGetDto>> GetAllPatientsAsync()
    {
        var patients = await _repo.GetAllPatientsAsync();
        return patients.Select(MapperService.Converter).ToList();
    }

    public async Task UpdatePatientAsync(PatientUpdateDto patient, long userId)
    {
        var patientEntity = await _repo.GetPatientByUserIdAsync(userId);
        patientEntity.DateOfBirth = patient.DateOfBirth;
        patientEntity.Gender = patient.Gender;
        await _repo.UpdatePatientAsync(patientEntity);
    }
}

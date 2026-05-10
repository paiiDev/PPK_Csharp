using JWT_EncDec_Example.DTOs;
using JWT_EncDec_Example.DummyData;
using JWT_EncDec_Example.Helpers.EncryptService;

namespace JWT_EncDec_Example.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly IEncryptionService _encryptionService;
        public StudentService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }
        public StudentResponseDto CreateStudent(StudentRequestDto request)
        {

            var encryptedPhoneNumber = _encryptionService.Encrypt(request.PhoneNumber);
            var encryptedEmail = _encryptionService.Encrypt(request.Email);
            var encryptedNrc = _encryptionService.Encrypt(request.Nrc);

            var encryptedData = new StudentResponseDto
            {
                Id = request.Id,
                Name = request.Name,
                PhoneNumber = encryptedPhoneNumber,
                Email = encryptedEmail,
                Nrc = encryptedNrc

            };
            
           StudentDummyData.Students.Add(encryptedData);
            return encryptedData;

        }
    }
}

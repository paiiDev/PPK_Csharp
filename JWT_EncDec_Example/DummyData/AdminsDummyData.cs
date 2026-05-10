using JWT_EncDec_Example.DTOs;

namespace JWT_EncDec_Example.DummyData
{
    public static class AdminsDummyData
    {
        public static List<AdminModel> Admins { get; } = new List<AdminModel>
        {
            new AdminModel { Id = 1,UserName = "admin", Password = "admin123", Role = "Admin" },
            new AdminModel { Id = 2 ,UserName = "admin2", Password = "admin123", Role = "Admin" },
        };
    }
}

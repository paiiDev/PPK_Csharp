using Dapper;

DapperService service = new DapperService("Data Source=.;Initial Catalog=DotNetDatabase;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");
//service.Create("Chole", 23);
//service.ReadAllData();
//service.Read(1);
//service.Update(4, "Chole", 24);
service.Delete(4);
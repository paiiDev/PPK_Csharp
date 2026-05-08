
using Effortless.Net.Encryption;
using EncDecExample.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// 1. Create a builder
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddUserSecrets<Program>();
string mySecretKey = builder.Configuration["EncryptionSettings:EncryptionKey"]!;
Console.WriteLine($"Key: {mySecretKey}");

// 2. Register your custom service with a lifetime (Transient, Scoped, or Singleton)
builder.Services.AddTransient<IEncryptionService, EncryptionService>();

// 3. Build the host
using IHost host = builder.Build();

// 4. Resolve and use the service
var myService = host.Services.GetRequiredService<IEncryptionService>();

Console.Write("Please enter your password: ");
string password = Console.ReadLine() ?? string.Empty;

string encryptedPwd = myService.Encrypt(password, mySecretKey);

Console.WriteLine($"Encrypted password: {encryptedPwd}");

string decryptedPwd = myService.Decrypt(encryptedPwd, mySecretKey);

Console.WriteLine($"Decrpted password; {decryptedPwd}");

await host.RunAsync();


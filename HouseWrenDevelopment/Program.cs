using HouseWrenDevelopment.Models.Contact;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
EmailServerConfig config = new EmailServerConfig
{
    SmtpPassword =  Environment.GetEnvironmentVariable("smtpPassword"),
    SmtpServer = "smtp.gmail.com",
    SmtpUsername = Environment.GetEnvironmentVariable("smtpUsername")
};

EmailAddress ToEmailAddress = new EmailAddress
{
    Address = Environment.GetEnvironmentVariable("smtpUsername"),
    Name = "HouseWrenDev"
};
builder.Services.AddSingleton<EmailServerConfig>(config);
builder.Services.AddTransient<IEmailService, MailKitEmailService>();
builder.Services.AddSingleton<EmailAddress>(ToEmailAddress);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

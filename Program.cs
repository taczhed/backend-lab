using System.Text;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = Environments.Production
});

var app = builder.Build();

app.MapGet("/people/", () => "A list of people");
app.MapGet("/people/{email:alpha}", (string email) => $"A single person with email {email}");
app.MapGet("/people/{age:range(0,100)}", (int age) => $"A list of people having {age} year(s)");
app.MapPut("/people/{email}", (string email) => $"Adding a person with email {email}");

app.Use(async (context, next) =>
{
    var requestContent = new StringBuilder();
    requestContent.Append("--- Request Info ---");
    requestContent.Append($"method = {context.Request.Method.ToUpper()}");
    requestContent.Append($"path = {context.Request.Path}");
    requestContent.Append("-- Headers --");

    foreach ( var (headerKey, headerValue) in context.Request.Headers )
    {
        requestContent.AppendLine($"header = {headerKey}, value = {headerValue}");
    }

    requestContent.Append("-- Body --");
    context.Request.EnableBuffering();
    var requestReader = new StreamReader(context.Request.Body);
    var content = await requestReader.ReadToEndAsync();
    requestContent.AppendLine($"body = {content}");
    Console.Write(requestContent.ToString());
    context.Request.Body.Position = 0;

    await next();
});

if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => "Hello Dev!");
}
else
{
    app.MapGet("/", () => "Hello Production!");
}

app.Run();
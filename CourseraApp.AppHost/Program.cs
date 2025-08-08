var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CourseraApp>("courseraapp");
builder.AddProject<Projects.CourseraSSRApp>("courserassrapp");

builder.Build().Run();

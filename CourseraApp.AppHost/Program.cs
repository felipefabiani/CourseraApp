var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.CourseraApp>("courseraapp");
//builder.AddProject<Projects.CourseraSSRApp>("courserassrapp");
builder.AddProject<Projects.EventEaseApp>("Event-Ease-App");

builder.Build().Run();

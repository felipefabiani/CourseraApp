var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CourseraApp>("courseraapp");

builder.Build().Run();

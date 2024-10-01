using Configuration;

var builder = WebApplication.CreateBuilder(args);

// Services config
ApiConfiguration.Configure(builder);

var app = builder.Build();

// App config (middlewares, endpoints, etc.)
ApiConfiguration.Configure(app);
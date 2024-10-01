using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers;
public static class Configuration
{
    public static string Connection { get; set; }
    public static string Application { get; set; }

    public static void AddSettings(IConfiguration configuration)
    {
        Connection = configuration["Connection"].ToString();
        Application = configuration["Application"].ToString();
    }
}
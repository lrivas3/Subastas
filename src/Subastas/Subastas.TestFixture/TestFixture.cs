﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subastas.Dependencies;

public class TestFixture
{
    public ServiceProvider ServiceProvider { get; private set; }

    public TestFixture()
    {
        var services = new ServiceCollection();

        // Cargar la configuración
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<TestFixture>()
            .Build();

        services.ConfigureServices(configuration);

        // Se utiliza unicamente para los proyectos de Test, no para el proyecto web
        // Ya que lo hace el propio proyecto
        services.AddSingleton((IConfiguration)configuration);

        ServiceProvider = services.BuildServiceProvider();
    }
}
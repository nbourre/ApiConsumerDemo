using DemoLibrary.Weather;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DemoLibrary
{
    /// Source : https://www.twilio.com/blog/2018/05/user-secrets-in-a-net-core-console-app.html
    /// TODO : For the teacher! Améliorer pour faciliter l'utilisation de clé secrète, car c'est hardcodé...
    /// <summary>
    /// Permet de retrouver un user secret tel que des clés API
    /// Pour ajouter un secret, on clique sur le projet avec le bouton de droit et
    /// on sélection "Manage User Secrets"
    /// Au moment d'écrire seulement une clé OpenWeatherMap fonctionne, copier la structure de AppSettings.json
    /// </summary>
    public class AppSecretConfigurations
    {
        private static readonly Lazy<AppSecretConfigurations> lazy = new Lazy<AppSecretConfigurations>(() => new AppSecretConfigurations());

        public static AppSecretConfigurations Instance { get { return lazy.Value; } }

        private static IConfigurationRoot Configuration;

        private static Dictionary<string, string> secrets = new Dictionary<string, string>();

        private AppSecretConfigurations()
        {   
            var devEnvVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            
            var isDevelopment = string.IsNullOrEmpty(devEnvVariable) || devEnvVariable.ToLower() == "development";

            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (isDevelopment)
            {
                builder.AddUserSecrets<AppSecretConfigurations>();
            }

            Configuration = builder.Build();

            Debug.WriteLine(Configuration.GetSection(nameof(Secrets)));
            

            IServiceCollection services = new ServiceCollection();

            //Map the implementations of your classes here ready for DI
            services
                .Configure<Secrets>(Configuration.GetSection(nameof(Secrets)))
                .AddOptions()
                .AddSingleton<ISecretRevealer, SecretRevealer>()
                .BuildServiceProvider();

            var serviceProvider = services.BuildServiceProvider();

            var revealer = serviceProvider.GetService<ISecretRevealer>();

            var kv = revealer.Reveal();

            secrets.Add(kv.Key, kv.Value);

        }

        public string GetSecret(string Key)
        {
            return secrets[Key];
        }
    }
}

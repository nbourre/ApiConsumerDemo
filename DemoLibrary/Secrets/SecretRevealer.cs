using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace DemoLibrary.Weather
{
    public class SecretRevealer : ISecretRevealer
    {
        private readonly Secrets _secrets;
        public SecretRevealer(IOptions<Secrets> secrets)
        {
            _secrets = secrets.Value ?? throw new ArgumentNullException(nameof(secrets));
        }

        public KeyValuePair<string, string> Reveal()
        {
            //I can now use my mapped secrets below.
            return new KeyValuePair<string, string>(nameof(_secrets.OWApiKey), _secrets.OWApiKey);
        }
    }
}
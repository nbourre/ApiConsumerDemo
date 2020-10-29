using System.Collections.Generic;

namespace DemoLibrary.Weather
{
    public interface ISecretRevealer
    {
        public KeyValuePair<string, string> Reveal();
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoLibrary
{
    interface IWeatherService<T>
    {
        public T GetData();
    }
}

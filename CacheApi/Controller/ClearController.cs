using CacheApi.Service;
using Nancy;
using System;

namespace CacheApi.Controller
{
    public class ClearController : NancyModule
    {
        public ClearController() : base("/clear")
        {
            // Requires Redis Admin Mode
            try
            {
                var obj = new ClearService();
                obj.ClearAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
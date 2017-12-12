using Nancy;
using System;

namespace CacheApi
{
    public class ClearController : NancyModule
    {
        public ClearController() : base("/clear")
        {
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
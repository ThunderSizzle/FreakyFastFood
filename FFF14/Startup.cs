using Owin;
using Microsoft.AspNet.SignalR;

namespace FFF
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
			ConfigureAuth( app );
			// Any connection or hub wire up and configuration should go here
			app.MapSignalR();
        }
    }
}

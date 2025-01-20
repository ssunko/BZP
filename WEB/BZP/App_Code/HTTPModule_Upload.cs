using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace BZPModule{
/// <summary>
/// Summary description for HTTPModule_Upload
/// </summary>
public class HTTPModule_Upload: IHttpModule{

    public void Init(HttpApplication app){
        app.BeginRequest += new EventHandler(app_BeginRequest);
    }

    void app_BeginRequest(object sender, EventArgs e){
        HttpContext context = ((HttpApplication)sender).Context;
        // check for size if more than 12.5 mb
        if (context.Request.ContentLength > 12800000){

            IServiceProvider provider = (IServiceProvider)context;
            HttpWorkerRequest wr = (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));

            // Check if body contains data
            if (wr.HasEntityBody()){
                // get the total body length
                int requestLength = wr.GetTotalEntityBodyLength();
                // Get the initial bytes loaded
                int initialBytes = wr.GetPreloadedEntityBodyLength();
                if (!wr.IsEntireEntityBodyIsPreloaded()){
                    byte[] buffer = new byte[512000];
                    // Set the received bytes to initial bytes before start reading
                    int receivedBytes = initialBytes;
                    while ((requestLength - receivedBytes) >= initialBytes){
                        // Read another set of bytes
                        initialBytes = wr.ReadEntityBody(buffer, buffer.Length);
                        // Update the received bytes
                        receivedBytes += initialBytes;
                    }
                    initialBytes = wr.ReadEntityBody(buffer, requestLength - receivedBytes);
                }
            }
            // Redirect the user to an error page from Page_Error event of page.
        }
    }

    public void Dispose(){}

} //  class HTTPModule_Upload
} //  namespace BZPModule


using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorAppDemo.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;

namespace BlazorAppDemo.Client.Pages
{
    public partial class Loader
    {
        protected FileData fileData;
        protected bool isLoaded;

        protected async Task ReadFile()
        {
            var dotNetReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("processFile", dotNetReference, "#file");
        }

        [JSInvokable("ProcessFileResult")]
        public async Task  ProcessFileResult(FileData info)
        {
            fileData = info;
            isLoaded = true;
            Console.WriteLine(fileData.Name);
            StateHasChanged();
            await Upload();
        }

        private async Task Upload() 
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(fileData), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Http.PostAsync("Upload", content);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Uploaded");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }            
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }
    }
}

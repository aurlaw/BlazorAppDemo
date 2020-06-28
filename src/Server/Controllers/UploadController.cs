using BlazorAppDemo.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.RegularExpressions;
using SystemFile = System.IO.File;

namespace BlazorAppDemo.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<UploadController> logger;
        public UploadController(ILogger<UploadController> logger)
        {
            this.logger = logger;
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FileData data)
        {
            var fileName = await SaveFile(data);
            return Ok(new {IsSuccess = true, File = fileName});
        }

        private Task<string> SaveFile(FileData data)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));

            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileName = Path.Combine(path, string.Concat(milliseconds.ToString(), "_", data.Name));
            logger.LogInformation($"Uploading file to {fileName}");
            var base64Data = Regex.Match(data.DataUrl, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            if(!SystemFile.Exists(fileName))
            {
                SystemFile.WriteAllBytes(fileName, binData);
                logger.LogInformation("File Saved");
            }
            return Task.FromResult(fileName);
        }

    }
}
/*
var base64Data = Regex.Match(stringName, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        var binData = Convert.FromBase64String(base64Data);

        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //filename = Path.Combine(path, base64Data.Replace(@"/", string.Empty));

        long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        string fileName = "Sn" + milliseconds.ToString() + ".PNG";
        filename = Path.Combine(path, fileName);

        if (!File.Exists(filename))
        {
            //using (var stream = new MemoryStream(binData))
            //{
                File.WriteAllBytes(filename, binData);
            //}
        }

        newItem.ItemValue = filename;
*/
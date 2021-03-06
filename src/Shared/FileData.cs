﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazorAppDemo.Shared
{
    public class FileData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        [JsonPropertyName("dataURL")]
        public string DataUrl { get; set; }

        public bool IsImage => Type != null && (Type.Contains("png") || Type.Contains("gif") || Type.Contains("jpg") || Type.Contains("jpeg"));
    }
}

//        var output = {name: file.name, type: file.type, size: file.size, dataURL: dataURL};

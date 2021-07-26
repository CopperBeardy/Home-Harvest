using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHarvest.Shared.Dtos
{
    public class UploadedFileDto
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }
}

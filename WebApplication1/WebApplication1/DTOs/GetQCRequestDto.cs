using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DTOs
{
    public class GetQCRequestDto
    {
        public int IndexId { get; set; }
        public string UserBarcode { get; set; }
        public string Barcode { get; set; }
    }
}
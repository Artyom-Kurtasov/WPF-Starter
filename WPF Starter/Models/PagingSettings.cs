using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Starter.Models
{
    public class PagingSettings
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; } = 1000;
    }
}

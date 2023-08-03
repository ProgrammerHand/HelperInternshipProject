using Helper.Application.Abstractions;
using Helper.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Application.Queries
{
    public class GetInquiry : IQuery<InquiryDto>
    {
        public int Id { get; set; }
    }
}

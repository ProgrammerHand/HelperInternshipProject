﻿using Helper.Core.User;

namespace Helper.Application.DTO
{
    public sealed record InquiryDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime RequestedStartDate { get; set; }
        public DateTime? RequestedEndDate { get; set; }
        public string SolutionDecision { get; set; }
        public string? FeasibilityNote { get; set; }
        public string AcceptanceStatus { get; set; }
        public Core.User.User Author { get; set; }
        public byte[] RowVersion { get; set; }
    }
}

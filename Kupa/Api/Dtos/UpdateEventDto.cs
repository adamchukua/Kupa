﻿using Kupa.Api.Enums;

namespace Kupa.Api.Dtos
{
    public class UpdateEventDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public LocationTypeId LocationTypeId { get; set; }

        public string Location { get; set; }
    }
}

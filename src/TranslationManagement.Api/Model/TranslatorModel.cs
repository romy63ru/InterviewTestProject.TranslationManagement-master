﻿using static TranslationManagement.Api.Repository;

namespace TranslationManagement.Api
{
    public class TranslatorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HourlyRate { get; set; }
        public TranslatorStatus Status { get; set; }
        public string CreditCardNumber { get; set; }
    }
}

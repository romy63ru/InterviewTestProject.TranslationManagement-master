
using TranslationManagement.Api;

public interface IBusinessRules
{
    bool IsValid(TranslationJob job, TranslatorModel translator, string newStatus = "");
}

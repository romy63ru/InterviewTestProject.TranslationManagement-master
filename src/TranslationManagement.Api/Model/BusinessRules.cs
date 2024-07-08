using System.Linq;

namespace TranslationManagement.Api.Model;

public class BusinessRules : IBusinessRules
{
    public bool IsValid(TranslationJob job, TranslatorModel translator, string newStatus = "")
    {
        if (job == null || translator == null)
        {
            return false;
        }

        if (typeof(JobStatuses).GetProperties().Count(prop => prop.Name == newStatus) == 0)
        {
            return false;
        }

        bool isInvalidStatusChange = (job.Status == JobStatuses.New && newStatus == JobStatuses.Completed) ||
                                     job.Status == JobStatuses.Completed || newStatus == JobStatuses.New;
        if (isInvalidStatusChange)
        {
            return false;
        }

        //only Certified translators can work on jobs
        if (job.Status == JobStatuses.Inprogress && newStatus == JobStatuses.New && translator.Status != TranslatorStatus.Certified)
        {
            return false;
        }

        return true;
    }
}
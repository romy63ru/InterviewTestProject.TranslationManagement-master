import { http } from "./http";
import { TranslationJob } from "./TranslationJob";

export const allTranslationJobs = async (): Promise<TranslationJob[]> => {
  let translationJob: TranslationJob[] = [];
  const response = await fetch("http://localhost:5000/api/jobs/GetJobs");
  translationJob = await response.json();
  return translationJob.map((job) => ({ ...job })); // Replace this with your actual return value
};

export const createJob = async (
  job: TranslationJob
): Promise<TranslationJob | undefined> => {
  const result = await http<TranslationJob, TranslationJob>({
    path: "/jobs/CreateJob",
    method: "post",
    body: job,
  });
  if (result.ok && result.body) {
    return result.body;
  } else {
    return undefined;
  }
};

import React from "react";
import { TranslationJob } from "./TranslationJob";
import { allTranslationJobs } from "./Data";
import { JobList } from "./JobList";

export const HomePage = () => {
  const [jobs, setJobs] = React.useState<TranslationJob[]>([]);
  const [jobsLoading, setJobsLoading] = React.useState(true);

  React.useEffect(() => {
    let cancelled = false;
    const doGetTranslationJob = async () => {
      const allJobs = await allTranslationJobs();
      if (!cancelled) {
        setJobs(allJobs);
        setJobsLoading(false);
      }
    };
    doGetTranslationJob();
    return () => {
      cancelled = true;
    };
  }, []);

  return (
    <div>{jobsLoading ? <div>Loading...</div> : <JobList data={jobs} />}</div>
  );
};

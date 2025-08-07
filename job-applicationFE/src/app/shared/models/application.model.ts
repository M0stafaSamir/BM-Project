export interface ApplyModel {
  jobId: number;
  CoverLetter?: string;
  appliedAt: Date;
  resumeFile?: File;
}

export interface GetApplication {
  id: number;
  jobId: number;
  applicant: string;
  jobTitle: string;
  coverLetter?: string;
  appliedAt: Date;
  Resume: string;
}

export interface GetJob {
  id: string;
  title: string;
  description?: string;
  location?: string;
  company: string;
  salary?: number;
  applied?: number;
  email: string;
  postedAt: Date;
  createdByName: string;
}
export interface createJob {
  title: string;
  description?: string;
  location?: string;
  company: string;
  salary?: number;
  email: string;
  postedAt: Date;
  createdById: string;
}

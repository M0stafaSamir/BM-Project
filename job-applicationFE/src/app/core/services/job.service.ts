import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from './environment';
import { createJob, updateJob } from '../../shared/models/job.model';

@Injectable({
  providedIn: 'root',
})
export class JobService {
  constructor(private httpClient: HttpClient) {}
  private apiUrl = `${environment.apiUrl}/job`;

  getAllJobs() {
    return this.httpClient.get(`${this.apiUrl}/getAll`);
  }
  getJobById(id: number) {
    return this.httpClient.get(`${this.apiUrl}/getById/${id}`);
  }
  createJob(jobData: createJob) {
    return this.httpClient.post(`${this.apiUrl}/create`, jobData);
  }
  updateJob(jobData: any, id: number) {
    return this.httpClient.put(`${this.apiUrl}/update/${id}`, jobData);
  }
}

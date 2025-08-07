import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from './environment';
import { ApplyModel } from '../../shared/models/application.model';

@Injectable({
  providedIn: 'root',
})
export class ApplicationService {
  constructor(private httpClient: HttpClient) {}

  private apiUrl = `${environment.apiUrl}/application`;

  applyForJob(applicationData: FormData) {
    return this.httpClient.post(`${this.apiUrl}/apply`, applicationData);
  }
  getUserApplications() {
    return this.httpClient.get(`${this.apiUrl}/myApplications`);
  }
  getApplicationsByJobId(jobId: number) {
    return this.httpClient.get(`${this.apiUrl}/JobApplication/${jobId}`);
  }
  deleteApplication(applicationId: number) {
    return this.httpClient.delete(
      `${this.apiUrl}/deleteApplication/${applicationId}`
    );
  }
}

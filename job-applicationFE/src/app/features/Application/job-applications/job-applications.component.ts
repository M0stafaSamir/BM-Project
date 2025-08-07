import { ApplicationService } from './../../../core/services/application.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { GetApplication } from '../../../shared/models/application.model';

@Component({
  selector: 'app-job-applications',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './job-applications.component.html',
  styleUrl: './job-applications.component.css',
})
export class JobApplicationsComponent implements OnInit {
  constructor(
    private appService: ApplicationService,
    private route: ActivatedRoute
  ) {}

  jobId: number = 0;
  applications!: GetApplication[];

  ngOnInit(): void {
    this.jobId = Number(this.route.snapshot.params['id']);

    this.appService.getApplicationsByJobId(this.jobId).subscribe({
      next: (res: any) => {
        console.log('Applications for Job:', res.data);
        this.applications = res.data;
      },
      error: (err) => {
        console.error('Error fetching job applications', err);
      },
    });
  }
}

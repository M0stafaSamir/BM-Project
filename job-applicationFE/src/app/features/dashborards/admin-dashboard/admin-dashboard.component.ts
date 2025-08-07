import { CommonModule } from '@angular/common';
import { GetJob } from '../../../shared/models/job.model';
import { JobService } from './../../../core/services/job.service';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ApplicationService } from '../../../core/services/application.service';
import { GetApplication } from '../../../shared/models/application.model';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.css',
})
export class AdminDashboardComponent implements OnInit {
  constructor(
    private jobService: JobService,
    private appService: ApplicationService
  ) {}

  jobs!: GetJob[];
  ngOnInit(): void {
    this.jobService.getAllJobs().subscribe({
      next: (res: any) => {
        this.jobs = res.data;
        console.log('Jobs:', this.jobs);
      },
      error: (err) => {
        console.error('Error fetching jobs', err);
      },
    });
  }
}
